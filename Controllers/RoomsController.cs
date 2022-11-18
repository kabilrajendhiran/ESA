using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESA.Data;
using ESA.Models;
using ESA.Dto;

namespace ESA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ESAContext _context;

        public RoomsController(ESAContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var results = await _context.RoomAndSeats.ToListAsync();
            return Ok(new { Data = results });
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostRoom(SaveRoomDto roomDto)
        {
            //_context.Rooms.Add(room);
            //await _context.SaveChangesAsync();
            //return CreatedAtAction("GetRoom", new { id = room.Id }, room);

            if(await _context.Rooms.AnyAsync(x => x.RoomNumber == roomDto.RoomNumber))
            {
                return BadRequest("Room Already Exist");
            } 


            using(var transaction =await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var room = new Room()
                    {
                        RoomNumber = roomDto.RoomNumber,
                        Capacity = roomDto.Row * roomDto.Col,
                        BlockId = roomDto.BlockId,
                        Row = roomDto.Row,
                        Col = roomDto.Col
                    };

                    await _context.Rooms.AddAsync(room);
                    await _context.SaveChangesAsync();

                    int roomId = room.Id;

                    foreach (SeatsDTO se in roomDto.Seats)
                    {
                        var roomSeat = new RoomSeat()
                        {
                            RoomId = roomId,
                            SeatId = se.Id,
                            Optional = se.Optional
                        };

                        await _context.RoomSeats.AddAsync(roomSeat);

                        Console.WriteLine(se);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return Problem(e.Message);
                }
            }

            return Ok(new { Message = "Success" });        
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
