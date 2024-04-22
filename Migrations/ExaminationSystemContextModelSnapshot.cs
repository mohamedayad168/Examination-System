﻿// <auto-generated />
using System;
using Examination_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Examination_System.Migrations
{
    [DbContext(typeof(ExaminationSystemContext))]
    partial class ExaminationSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Examination_System.Models.Branch", b =>
                {
                    b.Property<int>("BrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("br_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrId"));

                    b.Property<string>("BrDescription")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("br_description");

                    b.Property<string>("BrName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("br_name");

                    b.HasKey("BrId")
                        .HasName("PK__branch__E78B89906AF4371C");

                    b.ToTable("branch", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.Course", b =>
                {
                    b.Property<int>("CrsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("crs_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CrsId"));

                    b.Property<int>("CrsGrade")
                        .HasColumnType("int")
                        .HasColumnName("crs_grade");

                    b.Property<string>("CrsName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("crs_name");

                    b.HasKey("CrsId")
                        .HasName("PK__Course__ECAF5375EBBACB5E");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.Crsdepin", b =>
                {
                    b.Property<int>("DepId")
                        .HasColumnType("int")
                        .HasColumnName("dep_id");

                    b.Property<int>("CrsId")
                        .HasColumnType("int")
                        .HasColumnName("crs_id");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int")
                        .HasColumnName("instructor_id");

                    b.HasKey("DepId", "CrsId", "InstructorId")
                        .HasName("PK__CRSDEPIN__1C20C2991C348141");

                    b.HasIndex("CrsId");

                    b.HasIndex("InstructorId");

                    b.ToTable("CRSDEPINS", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.Crstopic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("topic_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TopicId"));

                    b.Property<int?>("CrsId")
                        .HasColumnType("int")
                        .HasColumnName("crs_id");

                    b.Property<string>("TopicName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("topic_name");

                    b.HasKey("TopicId")
                        .HasName("PK__CRSTopic__D5DAA3E951BBB267");

                    b.HasIndex("CrsId");

                    b.ToTable("CRSTopics", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.Department", b =>
                {
                    b.Property<int>("DepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("dep_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepId"));

                    b.Property<int?>("BrNo")
                        .HasColumnType("int")
                        .HasColumnName("br_no");

                    b.Property<string>("DepDescription")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("dep_description");

                    b.Property<string>("DepName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("dep_name");

                    b.Property<int?>("MgrNo")
                        .HasColumnType("int")
                        .HasColumnName("mgr_no");

                    b.HasKey("DepId")
                        .HasName("PK__departme__BB4BD8F8B127B60F");

                    b.HasIndex("BrNo");

                    b.HasIndex("MgrNo");

                    b.ToTable("department", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.Exam", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("exam_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamId"));

                    b.Property<int>("CrsId")
                        .HasColumnType("int")
                        .HasColumnName("crs_id");

                    b.Property<int>("Duration")
                        .HasColumnType("int")
                        .HasColumnName("duration");

                    b.Property<DateOnly>("GenerationDate")
                        .HasColumnType("date")
                        .HasColumnName("exam_date");

                    b.HasKey("ExamId")
                        .HasName("PK__exam__9C8C7BE9D7A25981");

                    b.HasIndex("CrsId");

                    b.ToTable("exam", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.ExamQuestion", b =>
                {
                    b.Property<int>("ExamId")
                        .HasColumnType("int")
                        .HasColumnName("exam_id");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int")
                        .HasColumnName("question_id");

                    b.Property<int>("Degree")
                        .HasColumnType("int")
                        .HasColumnName("degree");

                    b.HasKey("ExamId", "QuestionId")
                        .HasName("PK__exam_que__1E605ABD2CB41DA4");

                    b.HasIndex("QuestionId");

                    b.ToTable("exam_questions", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .HasColumnType("int")
                        .HasColumnName("instructor_id");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("salary");

                    b.HasKey("InstructorId")
                        .HasName("PK__instruct__A1EF56E83599B834");

                    b.ToTable("instructor", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("question_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionId"));

                    b.Property<int?>("CrsId")
                        .HasColumnType("int")
                        .HasColumnName("crs_id");

                    b.Property<int>("QuestionAnswer")
                        .HasColumnType("int")
                        .HasColumnName("question_answer");

                    b.Property<string>("QuestionText")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("question_text");

                    b.Property<string>("QuestionType")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("question_type");

                    b.HasKey("QuestionId")
                        .HasName("PK__question__2EC2154983C54507");

                    b.HasIndex("CrsId");

                    b.ToTable("question", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.QuestionOption", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int")
                        .HasColumnName("question_id");

                    b.Property<int>("OptionNo")
                        .HasColumnType("int")
                        .HasColumnName("option_no");

                    b.Property<string>("OptionText")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("option_text");

                    b.HasKey("QuestionId", "OptionNo")
                        .HasName("PK__question__318CBDDAEB03D870");

                    b.ToTable("question_options", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.Student", b =>
                {
                    b.Property<int>("StdId")
                        .HasColumnType("int")
                        .HasColumnName("std_id");

                    b.HasKey("StdId")
                        .HasName("PK__student__0B0245BA69EFE86C");

                    b.ToTable("student", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.StudentAnswer", b =>
                {
                    b.Property<int>("ExamId")
                        .HasColumnType("int")
                        .HasColumnName("exam_id");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int")
                        .HasColumnName("question_id");

                    b.Property<int>("StudentId")
                        .HasColumnType("int")
                        .HasColumnName("std_id");

                    b.Property<int>("SelectedOption")
                        .HasColumnType("int")
                        .HasColumnName("selected_option");

                    b.HasKey("ExamId", "QuestionId", "StudentId")
                        .HasName("PK__student___854A69BB8F3670B8");

                    b.HasIndex("QuestionId");

                    b.HasIndex("StudentId");

                    b.ToTable("student_answer", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.StudentCourse", b =>
                {
                    b.Property<int>("CrsId")
                        .HasColumnType("int")
                        .HasColumnName("crs_id");

                    b.Property<int>("StudentId")
                        .HasColumnType("int")
                        .HasColumnName("student_id");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("grade");

                    b.HasKey("CrsId", "StudentId")
                        .HasName("PK__StudentC__5E0C631CE6F705D6");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourse", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.StudentExam", b =>
                {
                    b.Property<int>("ExamId")
                        .HasColumnType("int")
                        .HasColumnName("exam_id");

                    b.Property<int>("StdId")
                        .HasColumnType("int")
                        .HasColumnName("std_id");

                    b.Property<DateOnly>("ExamDate")
                        .HasColumnType("date")
                        .HasColumnName("exam_date");

                    b.Property<int>("Grade")
                        .HasColumnType("int")
                        .HasColumnName("grade");

                    b.HasKey("ExamId", "StdId")
                        .HasName("PK__student___2C3C5FB2B70B6D2C");

                    b.HasIndex("StdId");

                    b.ToTable("student_exam", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("UserAddress")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_address");

                    b.Property<int>("UserAge")
                        .HasColumnType("int")
                        .HasColumnName("user_age");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_name");

                    b.Property<string>("UserPass")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_pass");

                    b.Property<string>("UserPhone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("user_phone");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId")
                        .HasName("PK__users__B9BE370FE7CA91AA");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("WorkIn", b =>
                {
                    b.Property<int>("DepNo")
                        .HasColumnType("int")
                        .HasColumnName("dep_no");

                    b.Property<int>("InsNo")
                        .HasColumnType("int")
                        .HasColumnName("ins_no");

                    b.HasKey("DepNo", "InsNo")
                        .HasName("PK__work_in__5280989D30DC0ED1");

                    b.HasIndex("InsNo");

                    b.ToTable("work_in", (string)null);
                });

            modelBuilder.Entity("Examination_System.Models.Crsdepin", b =>
                {
                    b.HasOne("Examination_System.Models.Course", "Crs")
                        .WithMany("Crsdepins")
                        .HasForeignKey("CrsId")
                        .IsRequired()
                        .HasConstraintName("FK__CRSDEPINS__crs_i__03F0984C");

                    b.HasOne("Examination_System.Models.Department", "Dep")
                        .WithMany("Crsdepins")
                        .HasForeignKey("DepId")
                        .IsRequired()
                        .HasConstraintName("FK__CRSDEPINS__dep_i__02FC7413");

                    b.HasOne("Examination_System.Models.Instructor", "Instructor")
                        .WithMany("Crsdepins")
                        .HasForeignKey("InstructorId")
                        .IsRequired()
                        .HasConstraintName("FK__CRSDEPINS__instr__04E4BC85");

                    b.Navigation("Crs");

                    b.Navigation("Dep");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("Examination_System.Models.Crstopic", b =>
                {
                    b.HasOne("Examination_System.Models.Course", "Crs")
                        .WithMany("Crstopics")
                        .HasForeignKey("CrsId")
                        .HasConstraintName("FK__CRSTopics__crs_i__00200768");

                    b.Navigation("Crs");
                });

            modelBuilder.Entity("Examination_System.Models.Department", b =>
                {
                    b.HasOne("Examination_System.Models.Branch", "BrNoNavigation")
                        .WithMany("Departments")
                        .HasForeignKey("BrNo")
                        .HasConstraintName("fk_department_branch");

                    b.HasOne("Examination_System.Models.Instructor", "MgrNoNavigation")
                        .WithMany("Departments")
                        .HasForeignKey("MgrNo")
                        .HasConstraintName("fk_department_manager");

                    b.Navigation("BrNoNavigation");

                    b.Navigation("MgrNoNavigation");
                });

            modelBuilder.Entity("Examination_System.Models.Exam", b =>
                {
                    b.HasOne("Examination_System.Models.Course", "Crs")
                        .WithMany("Exams")
                        .HasForeignKey("CrsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exam_course");

                    b.Navigation("Crs");
                });

            modelBuilder.Entity("Examination_System.Models.ExamQuestion", b =>
                {
                    b.HasOne("Examination_System.Models.Exam", "Exam")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("ExamId")
                        .IsRequired()
                        .HasConstraintName("fk_exam_questions_exam");

                    b.HasOne("Examination_System.Models.Question", "Question")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("QuestionId")
                        .IsRequired()
                        .HasConstraintName("fk_exam_questions_question");

                    b.Navigation("Exam");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Examination_System.Models.Instructor", b =>
                {
                    b.HasOne("Examination_System.Models.User", "InstructorNavigation")
                        .WithOne("Instructor")
                        .HasForeignKey("Examination_System.Models.Instructor", "InstructorId")
                        .IsRequired()
                        .HasConstraintName("fk_instructor_user");

                    b.Navigation("InstructorNavigation");
                });

            modelBuilder.Entity("Examination_System.Models.Question", b =>
                {
                    b.HasOne("Examination_System.Models.Course", "Crs")
                        .WithMany("Questions")
                        .HasForeignKey("CrsId")
                        .HasConstraintName("fk_question_course");

                    b.Navigation("Crs");
                });

            modelBuilder.Entity("Examination_System.Models.QuestionOption", b =>
                {
                    b.HasOne("Examination_System.Models.Question", "Question")
                        .WithMany("QuestionOptions")
                        .HasForeignKey("QuestionId")
                        .IsRequired()
                        .HasConstraintName("fk_question_options_question");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Examination_System.Models.Student", b =>
                {
                    b.HasOne("Examination_System.Models.User", "Std")
                        .WithOne("Student")
                        .HasForeignKey("Examination_System.Models.Student", "StdId")
                        .IsRequired()
                        .HasConstraintName("fk_student_user");

                    b.Navigation("Std");
                });

            modelBuilder.Entity("Examination_System.Models.StudentAnswer", b =>
                {
                    b.HasOne("Examination_System.Models.Exam", "Exam")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("ExamId")
                        .IsRequired()
                        .HasConstraintName("fk_student_answer_exam");

                    b.HasOne("Examination_System.Models.Question", "Question")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("QuestionId")
                        .IsRequired()
                        .HasConstraintName("fk_student_answer_question");

                    b.HasOne("Examination_System.Models.Student", "Student")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("fk_student_answer_student");

                    b.Navigation("Exam");

                    b.Navigation("Question");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Examination_System.Models.StudentCourse", b =>
                {
                    b.HasOne("Examination_System.Models.Course", "Crs")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CrsId")
                        .IsRequired()
                        .HasConstraintName("FK__StudentCo__crs_i__7C4F7684");

                    b.HasOne("Examination_System.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK__StudentCo__stude__7D439ABD");

                    b.Navigation("Crs");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Examination_System.Models.StudentExam", b =>
                {
                    b.HasOne("Examination_System.Models.Exam", "Exam")
                        .WithMany("StudentExams")
                        .HasForeignKey("ExamId")
                        .IsRequired()
                        .HasConstraintName("fk_student_exam_exam");

                    b.HasOne("Examination_System.Models.Student", "Std")
                        .WithMany("StudentExams")
                        .HasForeignKey("StdId")
                        .IsRequired()
                        .HasConstraintName("fk_student_exam_student");

                    b.Navigation("Exam");

                    b.Navigation("Std");
                });

            modelBuilder.Entity("WorkIn", b =>
                {
                    b.HasOne("Examination_System.Models.Department", null)
                        .WithMany()
                        .HasForeignKey("DepNo")
                        .IsRequired()
                        .HasConstraintName("fk_workin_dep");

                    b.HasOne("Examination_System.Models.Instructor", null)
                        .WithMany()
                        .HasForeignKey("InsNo")
                        .IsRequired()
                        .HasConstraintName("fk_ins_work");
                });

            modelBuilder.Entity("Examination_System.Models.Branch", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("Examination_System.Models.Course", b =>
                {
                    b.Navigation("Crsdepins");

                    b.Navigation("Crstopics");

                    b.Navigation("Exams");

                    b.Navigation("Questions");

                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("Examination_System.Models.Department", b =>
                {
                    b.Navigation("Crsdepins");
                });

            modelBuilder.Entity("Examination_System.Models.Exam", b =>
                {
                    b.Navigation("ExamQuestions");

                    b.Navigation("StudentAnswers");

                    b.Navigation("StudentExams");
                });

            modelBuilder.Entity("Examination_System.Models.Instructor", b =>
                {
                    b.Navigation("Crsdepins");

                    b.Navigation("Departments");
                });

            modelBuilder.Entity("Examination_System.Models.Question", b =>
                {
                    b.Navigation("ExamQuestions");

                    b.Navigation("QuestionOptions");

                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("Examination_System.Models.Student", b =>
                {
                    b.Navigation("StudentAnswers");

                    b.Navigation("StudentCourses");

                    b.Navigation("StudentExams");
                });

            modelBuilder.Entity("Examination_System.Models.User", b =>
                {
                    b.Navigation("Instructor");

                    b.Navigation("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
