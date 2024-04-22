using Examination_System.Data;
using Examination_System.ViweModels;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repos
{
    public interface IReportsRepo
    {
        public IQueryable<GetCourseAndStudents> GetInstCoursAndStdReport(int instId);
        public IQueryable<StudentGrades> GetStudentGradesReport(int stdId);

        public IQueryable<GetStudentsByDeptNum> GetStudentsByDeptNums(int deptNum);

        public IQueryable<GetCourseTopic> GetCourseTopics(int crsId);
        public IQueryable<GetExamAnswrs> get_exam_questions_with_student_answers(int ExamId, int stdId);

            public IQueryable<GetExamChoices> GetExamChoices(int ExamId);
    }
    public class ReportsRepo:IReportsRepo
    {
        
        
            readonly ExaminationSystemContext db;

            public ReportsRepo(ExaminationSystemContext _db)
            {
                db = _db;
            }

        public IQueryable<GetCourseAndStudents> GetInstCoursAndStdReport(int instId)
        {
            var data = db.Database.SqlQuery<GetCourseAndStudents>($"GetCoursesAndStudentCountByInstructor {instId}");

            return data;
        }

        public IQueryable<StudentGrades>GetStudentGradesReport(int stdId)
        {
            var data = db.Database.SqlQuery<StudentGrades>($"get_Std_results_by_stdId {stdId}");

            return data;
        }

        public IQueryable<GetStudentsByDeptNum>GetStudentsByDeptNums(int deptNum)
        {
            var data = db.Database.SqlQuery<GetStudentsByDeptNum>($"GetStudentsByDepartment {deptNum}");

            return data;
        }

        public IQueryable<GetCourseTopic> GetCourseTopics(int crsId)
        {
            var data = db.Database.SqlQuery<GetCourseTopic>($"GetCourseTopics {crsId}");

            return data;
        }


        public IQueryable<GetExamAnswrs> get_exam_questions_with_student_answers(int ExamId, int stdId )
        {
            var data = db.Database.SqlQuery<GetExamAnswrs>($"sp_get_exam_questions_with_student_answers {ExamId} , {stdId}");


            return data;
        }

        ///choices exam id 
        
        public IQueryable<GetExamChoices> GetExamChoices(int ExamId)
        {
            var data = db.Database.SqlQuery<GetExamChoices>($"sp_get_exam_questions_with_choices {ExamId}");

            return data;
        }


    }
}
