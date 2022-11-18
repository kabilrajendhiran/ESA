using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ESA.Models;

namespace ESA.Data
{
    public partial class ESAContext : DbContext
    {
        public ESAContext()
        {
        }

        public ESAContext(DbContextOptions<ESAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Block> Blocks { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<ExamRoom> ExamRooms { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomAndSeat> RoomAndSeats { get; set; } = null!;
        public virtual DbSet<RoomSeat> RoomSeats { get; set; } = null!;
        public virtual DbSet<RoomWiseStudent> RoomWiseStudents { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<SemesterSubject> SemesterSubjects { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentRoom> StudentRooms { get; set; } = null!;
        public virtual DbSet<StudentsByExam> StudentsByExams { get; set; } = null!;
        public virtual DbSet<StudentsCountByExam> StudentsCountByExams { get; set; } = null!;
        public virtual DbSet<StudentsCountExamDeptWise> StudentsCountExamDeptWises { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<SubjectAndDepartment> SubjectAndDepartments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=erp;Username=postgres;Password=#K@bil1998");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Block>(entity =>
            {
                entity.ToTable("block");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abbreviation).HasColumnName("abbreviation");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Fullname).HasColumnName("fullname");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("exam");

                entity.HasIndex(e => e.SubjectId, "subject_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.SemYear).HasColumnName("sem_year");

                entity.Property(e => e.Session).HasColumnName("session");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'pending'::text");

                entity.Property(e => e.SubjectId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("subject_id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("'semester_exam'::text");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("exam_subject_id_fkey");
            });

            modelBuilder.Entity<ExamRoom>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("exam_room");

                entity.HasIndex(e => e.ExamId, "exam_fk");

                entity.HasIndex(e => e.RoomId, "room_fk");

                entity.Property(e => e.ExamId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("exam_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.RoomId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("room_id");

                entity.HasOne(d => d.Exam)
                    .WithMany()
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("exam_room_exam_id_fkey");

                entity.HasOne(d => d.Room)
                    .WithMany()
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("exam_room_room_id_fkey");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.HasIndex(e => e.BlockId, "block_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BlockId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("block_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Col)
                    .HasColumnName("col")
                    .HasDefaultValueSql("4");

                entity.Property(e => e.MaxCapacity)
                    .HasColumnName("max_capacity")
                    .HasDefaultValueSql("28");

                entity.Property(e => e.RoomNumber).HasColumnName("room_number");

                entity.Property(e => e.Row)
                    .HasColumnName("row")
                    .HasDefaultValueSql("7");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("room_block_id_fkey");
            });

            modelBuilder.Entity<RoomAndSeat>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("room_and_seats");

                entity.Property(e => e.BlockId).HasColumnName("block_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Col).HasColumnName("col");

                entity.Property(e => e.MaxCapacity).HasColumnName("max_capacity");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.RoomNumber).HasColumnName("room_number");

                entity.Property(e => e.Row).HasColumnName("row");

                entity.Property(e => e.Seats)
                    .HasColumnType("json")
                    .HasColumnName("seats");
            });

            modelBuilder.Entity<RoomSeat>(entity =>
            {
                entity.ToTable("room_seat");

                entity.HasIndex(e => e.RoomId, "fki_romm_id_fk");

                entity.HasIndex(e => e.SeatId, "fki_seat_id_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Optional)
                    .HasColumnName("optional")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomSeats)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("romm_id_fk");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.RoomSeats)
                    .HasForeignKey(d => d.SeatId)
                    .HasConstraintName("seat_id_fk");
            });

            modelBuilder.Entity<RoomWiseStudent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("room_wise_students");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.Session).HasColumnName("session");

                entity.Property(e => e.Students)
                    .HasColumnType("json")
                    .HasColumnName("students");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("seat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Number).HasColumnName("number");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("semester");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DepartmentId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("department_id");

                entity.Property(e => e.Nth).HasColumnName("nth");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Semesters)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("semester_department_id_fkey");
            });

            modelBuilder.Entity<SemesterSubject>(entity =>
            {
                entity.ToTable("semester_subject");

                entity.HasIndex(e => e.SemesterId, "fkey");

                entity.HasIndex(e => e.SubjectId, "fki_s");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SemesterId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("semester_id");

                entity.Property(e => e.SubjectId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("subject_id");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.SemesterSubjects)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("semester_subject_semester_id_fkey");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SemesterSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("semester_subject_subject_id_fkey");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.HasIndex(e => e.SemesterId, "semester_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName).HasColumnName("first_name");

                entity.Property(e => e.LastName).HasColumnName("last_name");

                entity.Property(e => e.MiddleName).HasColumnName("middle_name");

                entity.Property(e => e.RegNo).HasColumnName("reg_no");

                entity.Property(e => e.RollNo).HasColumnName("roll_no");

                entity.Property(e => e.SemesterId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("semester_id");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_semester_id_fkey");
            });

            modelBuilder.Entity<StudentRoom>(entity =>
            {
                entity.ToTable("student_room");

                entity.HasIndex(e => e.StudentId, "student_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoomId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("room_id");

                entity.Property(e => e.StudentId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("student_id");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.StudentRooms)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_room_room_id_fkey");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentRooms)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_room_student_id_fkey");
            });

            modelBuilder.Entity<StudentsByExam>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("students_by_exam");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ExamId).HasColumnName("exam_id");

                entity.Property(e => e.RegNo).HasColumnName("reg_no");

                entity.Property(e => e.SemesterId).HasColumnName("semester_id");

                entity.Property(e => e.Session).HasColumnName("session");

                entity.Property(e => e.StudentId).HasColumnName("student_id");
            });

            modelBuilder.Entity<StudentsCountByExam>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("students_count_by_exam");

                entity.Property(e => e.AggregatedExamId).HasColumnName("aggregated_exam_id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.NumberOfStudents).HasColumnName("number_of_students");

                entity.Property(e => e.Session).HasColumnName("session");
            });

            modelBuilder.Entity<StudentsCountExamDeptWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("students_count_exam_dept_wise");

                entity.Property(e => e.Abbreviation).HasColumnName("abbreviation");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Elective).HasColumnName("elective");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Regulation).HasColumnName("regulation");
            });

            modelBuilder.Entity<SubjectAndDepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("subject_and_departments");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Details)
                    .HasColumnType("json")
                    .HasColumnName("details");

                entity.Property(e => e.Elective).HasColumnName("elective");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Regulation).HasColumnName("regulation");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
