using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination_System.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "branch",
                columns: table => new
                {
                    br_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    br_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    br_description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__branch__E78B89906AF4371C", x => x.br_id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    crs_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    crs_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    crs_grade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course__ECAF5375EBBACB5E", x => x.crs_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    user_pass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    user_age = table.Column<int>(type: "int", nullable: false),
                    user_phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    user_address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__B9BE370FE7CA91AA", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "CRSTopics",
                columns: table => new
                {
                    topic_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    topic_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    crs_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CRSTopic__D5DAA3E951BBB267", x => x.topic_id);
                    table.ForeignKey(
                        name: "FK__CRSTopics__crs_i__00200768",
                        column: x => x.crs_id,
                        principalTable: "Course",
                        principalColumn: "crs_id");
                });

            migrationBuilder.CreateTable(
                name: "exam",
                columns: table => new
                {
                    exam_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exam_date = table.Column<DateOnly>(type: "date", nullable: false),
                    crs_id = table.Column<int>(type: "int", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__exam__9C8C7BE9D7A25981", x => x.exam_id);
                    table.ForeignKey(
                        name: "fk_exam_course",
                        column: x => x.crs_id,
                        principalTable: "Course",
                        principalColumn: "crs_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_text = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    question_type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    crs_id = table.Column<int>(type: "int", nullable: true),
                    question_answer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__question__2EC2154983C54507", x => x.question_id);
                    table.ForeignKey(
                        name: "fk_question_course",
                        column: x => x.crs_id,
                        principalTable: "Course",
                        principalColumn: "crs_id");
                });

            migrationBuilder.CreateTable(
                name: "instructor",
                columns: table => new
                {
                    instructor_id = table.Column<int>(type: "int", nullable: false),
                    salary = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__instruct__A1EF56E83599B834", x => x.instructor_id);
                    table.ForeignKey(
                        name: "fk_instructor_user",
                        column: x => x.instructor_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    std_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__student__0B0245BA69EFE86C", x => x.std_id);
                    table.ForeignKey(
                        name: "fk_student_user",
                        column: x => x.std_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "exam_questions",
                columns: table => new
                {
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    degree = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__exam_que__1E605ABD2CB41DA4", x => new { x.exam_id, x.question_id });
                    table.ForeignKey(
                        name: "fk_exam_questions_exam",
                        column: x => x.exam_id,
                        principalTable: "exam",
                        principalColumn: "exam_id");
                    table.ForeignKey(
                        name: "fk_exam_questions_question",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "question_id");
                });

            migrationBuilder.CreateTable(
                name: "question_options",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false),
                    option_no = table.Column<int>(type: "int", nullable: false),
                    option_text = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__question__318CBDDAEB03D870", x => new { x.question_id, x.option_no });
                    table.ForeignKey(
                        name: "fk_question_options_question",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "question_id");
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    dep_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dep_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dep_description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    br_no = table.Column<int>(type: "int", nullable: true),
                    mgr_no = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__departme__BB4BD8F8B127B60F", x => x.dep_id);
                    table.ForeignKey(
                        name: "fk_department_branch",
                        column: x => x.br_no,
                        principalTable: "branch",
                        principalColumn: "br_id");
                    table.ForeignKey(
                        name: "fk_department_manager",
                        column: x => x.mgr_no,
                        principalTable: "instructor",
                        principalColumn: "instructor_id");
                });

            migrationBuilder.CreateTable(
                name: "student_answer",
                columns: table => new
                {
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    std_id = table.Column<int>(type: "int", nullable: false),
                    selected_option = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__student___854A69BB8F3670B8", x => new { x.exam_id, x.question_id, x.std_id });
                    table.ForeignKey(
                        name: "fk_student_answer_exam",
                        column: x => x.exam_id,
                        principalTable: "exam",
                        principalColumn: "exam_id");
                    table.ForeignKey(
                        name: "fk_student_answer_question",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "question_id");
                    table.ForeignKey(
                        name: "fk_student_answer_student",
                        column: x => x.std_id,
                        principalTable: "student",
                        principalColumn: "std_id");
                });

            migrationBuilder.CreateTable(
                name: "student_exam",
                columns: table => new
                {
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    std_id = table.Column<int>(type: "int", nullable: false),
                    grade = table.Column<int>(type: "int", nullable: false),
                    exam_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__student___2C3C5FB2B70B6D2C", x => new { x.exam_id, x.std_id });
                    table.ForeignKey(
                        name: "fk_student_exam_exam",
                        column: x => x.exam_id,
                        principalTable: "exam",
                        principalColumn: "exam_id");
                    table.ForeignKey(
                        name: "fk_student_exam_student",
                        column: x => x.std_id,
                        principalTable: "student",
                        principalColumn: "std_id");
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    crs_id = table.Column<int>(type: "int", nullable: false),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentC__5E0C631CE6F705D6", x => new { x.crs_id, x.student_id });
                    table.ForeignKey(
                        name: "FK__StudentCo__crs_i__7C4F7684",
                        column: x => x.crs_id,
                        principalTable: "Course",
                        principalColumn: "crs_id");
                    table.ForeignKey(
                        name: "FK__StudentCo__stude__7D439ABD",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "std_id");
                });

            migrationBuilder.CreateTable(
                name: "CRSDEPINS",
                columns: table => new
                {
                    dep_id = table.Column<int>(type: "int", nullable: false),
                    crs_id = table.Column<int>(type: "int", nullable: false),
                    instructor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CRSDEPIN__1C20C2991C348141", x => new { x.dep_id, x.crs_id, x.instructor_id });
                    table.ForeignKey(
                        name: "FK__CRSDEPINS__crs_i__03F0984C",
                        column: x => x.crs_id,
                        principalTable: "Course",
                        principalColumn: "crs_id");
                    table.ForeignKey(
                        name: "FK__CRSDEPINS__dep_i__02FC7413",
                        column: x => x.dep_id,
                        principalTable: "department",
                        principalColumn: "dep_id");
                    table.ForeignKey(
                        name: "FK__CRSDEPINS__instr__04E4BC85",
                        column: x => x.instructor_id,
                        principalTable: "instructor",
                        principalColumn: "instructor_id");
                });

            migrationBuilder.CreateTable(
                name: "work_in",
                columns: table => new
                {
                    dep_no = table.Column<int>(type: "int", nullable: false),
                    ins_no = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__work_in__5280989D30DC0ED1", x => new { x.dep_no, x.ins_no });
                    table.ForeignKey(
                        name: "fk_ins_work",
                        column: x => x.ins_no,
                        principalTable: "instructor",
                        principalColumn: "instructor_id");
                    table.ForeignKey(
                        name: "fk_workin_dep",
                        column: x => x.dep_no,
                        principalTable: "department",
                        principalColumn: "dep_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRSDEPINS_crs_id",
                table: "CRSDEPINS",
                column: "crs_id");

            migrationBuilder.CreateIndex(
                name: "IX_CRSDEPINS_instructor_id",
                table: "CRSDEPINS",
                column: "instructor_id");

            migrationBuilder.CreateIndex(
                name: "IX_CRSTopics_crs_id",
                table: "CRSTopics",
                column: "crs_id");

            migrationBuilder.CreateIndex(
                name: "IX_department_br_no",
                table: "department",
                column: "br_no");

            migrationBuilder.CreateIndex(
                name: "IX_department_mgr_no",
                table: "department",
                column: "mgr_no");

            migrationBuilder.CreateIndex(
                name: "IX_exam_crs_id",
                table: "exam",
                column: "crs_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_questions_question_id",
                table: "exam_questions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_crs_id",
                table: "question",
                column: "crs_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_answer_question_id",
                table: "student_answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_answer_std_id",
                table: "student_answer",
                column: "std_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_exam_std_id",
                table: "student_exam",
                column: "std_id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_student_id",
                table: "StudentCourse",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_in_ins_no",
                table: "work_in",
                column: "ins_no");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRSDEPINS");

            migrationBuilder.DropTable(
                name: "CRSTopics");

            migrationBuilder.DropTable(
                name: "exam_questions");

            migrationBuilder.DropTable(
                name: "question_options");

            migrationBuilder.DropTable(
                name: "student_answer");

            migrationBuilder.DropTable(
                name: "student_exam");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "work_in");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "exam");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "branch");

            migrationBuilder.DropTable(
                name: "instructor");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
