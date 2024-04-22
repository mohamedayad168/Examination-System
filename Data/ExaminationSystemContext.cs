using System;
using System.Collections.Generic;
using Examination_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Data;

public partial class ExaminationSystemContext : DbContext
{
    public ExaminationSystemContext()
    {
    }

    public ExaminationSystemContext(DbContextOptions<ExaminationSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Crsdepin> Crsdepins { get; set; }

    public virtual DbSet<Crstopic> Crstopics { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamQuestion> ExamQuestions { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<StudentExam> StudentExams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:iti-sql.database.windows.net,1433;Initial Catalog=Examination_System;Persist Security Info=False;User ID=admin3body;Password=Ash@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BrId).HasName("PK__branch__E78B89906AF4371C");

            entity.ToTable("branch");

            entity.Property(e => e.BrId).HasColumnName("br_id");
            entity.Property(e => e.BrDescription)
                .HasMaxLength(50)
                .HasColumnName("br_description");
            entity.Property(e => e.BrName)
                .HasMaxLength(50)
                .HasColumnName("br_name");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CrsId).HasName("PK__Course__ECAF5375EBBACB5E");

            entity.ToTable("Course");

            entity.Property(e => e.CrsId).HasColumnName("crs_id");
            entity.Property(e => e.CrsGrade).HasColumnName("crs_grade");
            entity.Property(e => e.CrsName)
                .HasMaxLength(255)
                .HasColumnName("crs_name");
        });

        modelBuilder.Entity<Crsdepin>(entity =>
        {
            entity.HasKey(e => new { e.DepId, e.CrsId, e.InstructorId }).HasName("PK__CRSDEPIN__1C20C2991C348141");

            entity.ToTable("CRSDEPINS");

            entity.Property(e => e.DepId).HasColumnName("dep_id");
            entity.Property(e => e.CrsId).HasColumnName("crs_id");
            entity.Property(e => e.InstructorId).HasColumnName("instructor_id");

            entity.HasOne(d => d.Crs).WithMany(p => p.Crsdepins)
                .HasForeignKey(d => d.CrsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CRSDEPINS__crs_i__03F0984C");

            entity.HasOne(d => d.Dep).WithMany(p => p.Crsdepins)
                .HasForeignKey(d => d.DepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CRSDEPINS__dep_i__02FC7413");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Crsdepins)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CRSDEPINS__instr__04E4BC85");
        });

        modelBuilder.Entity<Crstopic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__CRSTopic__D5DAA3E951BBB267");

            entity.ToTable("CRSTopics");

            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.CrsId).HasColumnName("crs_id");
            entity.Property(e => e.TopicName)
                .HasMaxLength(255)
                .HasColumnName("topic_name");

            entity.HasOne(d => d.Crs).WithMany(p => p.Crstopics)
                .HasForeignKey(d => d.CrsId)
                .HasConstraintName("FK__CRSTopics__crs_i__00200768");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepId).HasName("PK__departme__BB4BD8F8B127B60F");

            entity.ToTable("department");

            entity.Property(e => e.DepId).HasColumnName("dep_id");
            entity.Property(e => e.BrNo).HasColumnName("br_no");
            entity.Property(e => e.DepDescription)
                .HasMaxLength(50)
                .HasColumnName("dep_description");
            entity.Property(e => e.DepName)
                .HasMaxLength(50)
                .HasColumnName("dep_name");
            entity.Property(e => e.MgrNo).HasColumnName("mgr_no");

            entity.HasOne(d => d.BrNoNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.BrNo)
                .HasConstraintName("fk_department_branch");

            entity.HasOne(d => d.MgrNoNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.MgrNo)
                .HasConstraintName("fk_department_manager");

            entity.HasMany(d => d.InsNos).WithMany(p => p.DepNos)
                .UsingEntity<Dictionary<string, object>>(
                    "WorkIn",
                    r => r.HasOne<Instructor>().WithMany()
                        .HasForeignKey("InsNo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ins_work"),
                    l => l.HasOne<Department>().WithMany()
                        .HasForeignKey("DepNo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_workin_dep"),
                    j =>
                    {
                        j.HasKey("DepNo", "InsNo").HasName("PK__work_in__5280989D30DC0ED1");
                        j.ToTable("work_in");
                        j.IndexerProperty<int>("DepNo").HasColumnName("dep_no");
                        j.IndexerProperty<int>("InsNo").HasColumnName("ins_no");
                    });
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__exam__9C8C7BE9D7A25981");

            entity.ToTable("exam");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.CrsId).HasColumnName("crs_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.GenerationDate).HasColumnName("exam_date");

            entity.HasOne(d => d.Crs).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CrsId)
                .HasConstraintName("fk_exam_course");
        });

        modelBuilder.Entity<ExamQuestion>(entity =>
        {
            entity.HasKey(e => new { e.ExamId, e.QuestionId }).HasName("PK__exam_que__1E605ABD2CB41DA4");

            entity.ToTable("exam_questions");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Degree).HasColumnName("degree");

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamQuestions)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_exam_questions_exam");

            entity.HasOne(d => d.Question).WithMany(p => p.ExamQuestions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_exam_questions_question");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorId).HasName("PK__instruct__A1EF56E83599B834");

            entity.ToTable("instructor");

            entity.Property(e => e.InstructorId)
                .ValueGeneratedNever()
                .HasColumnName("instructor_id");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");

            entity.HasOne(d => d.InstructorNavigation).WithOne(p => p.Instructor)
                .HasForeignKey<Instructor>(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_instructor_user");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__question__2EC2154983C54507");

            entity.ToTable("question");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.CrsId).HasColumnName("crs_id");
            entity.Property(e => e.QuestionText)
                .IsUnicode(false)
                .HasColumnName("question_text");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("question_type");
            entity.Property(e => e.QuestionAnswer)
                .HasColumnName("question_answer");

            entity.HasOne(d => d.Crs).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CrsId)
                .HasConstraintName("fk_question_course");
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.OptionNo }).HasName("PK__question__318CBDDAEB03D870");

            entity.ToTable("question_options");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.OptionNo).HasColumnName("option_no");
            entity.Property(e => e.OptionText)
                .IsUnicode(false)
                .HasColumnName("option_text");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_question_options_question");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StdId).HasName("PK__student__0B0245BA69EFE86C");

            entity.ToTable("student");

            entity.Property(e => e.StdId)
                .ValueGeneratedNever()
                .HasColumnName("std_id");

            entity.HasOne(d => d.Std).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.StdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_user");
        });

        modelBuilder.Entity<StudentAnswer>(entity =>
        {
            entity.HasKey(e => new { e.ExamId, e.QuestionId, e.StudentId }).HasName("PK__student___854A69BB8F3670B8");

            entity.ToTable("student_answer");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.StudentId).HasColumnName("std_id");
            entity.Property(e => e.SelectedOption).HasColumnName("selected_option");

            entity.HasOne(d => d.Exam).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_answer_exam");

            entity.HasOne(d => d.Question).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_answer_question");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_answer_student");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => new { e.CrsId, e.StudentId }).HasName("PK__StudentC__5E0C631CE6F705D6");

            entity.ToTable("StudentCourse");

            entity.Property(e => e.CrsId).HasColumnName("crs_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.Grade)
                .HasColumnName("grade");

            entity.HasOne(d => d.Crs).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.CrsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__crs_i__7C4F7684");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__stude__7D439ABD");
        });

        modelBuilder.Entity<StudentExam>(entity =>
        {
            entity.HasKey(e => new { e.ExamId, e.StdId }).HasName("PK__student___2C3C5FB2B70B6D2C");

            entity.ToTable("student_exam");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.StdId).HasColumnName("std_id");
            entity.Property(e => e.ExamDate).HasColumnName("exam_date");
            entity.Property(e => e.Grade).HasColumnName("grade");

            entity.HasOne(d => d.Exam).WithMany(p => p.StudentExams)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_exam_exam");

            entity.HasOne(d => d.Std).WithMany(p => p.StudentExams)
                .HasForeignKey(d => d.StdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_exam_student");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370FE7CA91AA");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserAddress)
                .HasMaxLength(50)
                .HasColumnName("user_address");
            entity.Property(e => e.UserAge).HasColumnName("user_age");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPass)
                .HasMaxLength(50)
                .HasColumnName("user_pass");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(20)
                .HasColumnName("user_phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
