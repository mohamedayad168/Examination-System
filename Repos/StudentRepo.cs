using Examination_System.Data;
using Examination_System.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel;

namespace Examination_System.Repos
{
    public interface IStudentRepo
    {
        public Task<Exam> GetExamByCrsId(int crsId);
        public Task<Exam> GetExamById(int examId);
        public Task<bool> SubmitExam(int examId, int studentId, List<StudentAnswer> studentAnswers);
        public Task<StudentCourse> GetStudentCourseDegree(int examId, int studentId);
        public Task<bool> IsStudentExamSubmitted(int examId, int studentId);
        public Task<List<StudentCourse>> GetStudentCourses(int id);
        public Task<List<StudentCourse>> GetStudentResultsByStdId(int stdId);
        public Task<Exam> GetResultExam(int stdId, int crsId);
        public Task<List<string>> StudentAnswer(int examId, int studentId, List<int> qusetionId);
        public List<List<string>> ExamQuestionOptions(List<ExamQuestion> examQuestion);
        public List<string> ExamQuestionAnswers(List<ExamQuestion> examQuestion);
        public Task MarkExam(int examId, int studentId);
        public Task<User> GetUserById(int id);

    }

    public class StudentRepo : IStudentRepo
    {
        readonly ExaminationSystemContext db;

        public StudentRepo(ExaminationSystemContext _db)
        {
            db = _db;
        }

        public async Task<Exam> GetExamByCrsId(int crsId) //get exam by course id
        {
            try
            {
                // return the last exam added to the course
                return await db.Exams.Where(e => e.CrsId == crsId)
                    .Include(c => c.Crs)
                    .Include(e => e.ExamQuestions)
                    .ThenInclude(q => q.Question)
                    .ThenInclude(qo => qo.QuestionOptions)
                    .OrderByDescending(e => e.ExamId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Exam> GetExamById(int examId) //get exam by id
        {
            try
            {
                return await db.Exams.Where(e => e.ExamId == examId)
                    .Include(c => c.Crs)
                    .Include(e => e.ExamQuestions)
                    .ThenInclude(q => q.Question)
                    .ThenInclude(qo => qo.QuestionOptions)
                    .OrderByDescending(e => e.ExamId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> SubmitExam(int examId, int studentId, List<StudentAnswer> studentAnswers) //submit the exam
        {
            //int grade = 0;
            //float finalGrade = db.ExamQuestions.Where(eq => eq.ExamId == studentAnswers[0].ExamId).Sum(eq => eq.Degree); //get the total degree of the exam

            try
            {
                //int courseID = await db.Exams.Where(e => e.ExamId == examId).Select(e => e.CrsId).FirstOrDefaultAsync();
                //StudentCourse markedExam = await db.StudentCourses.Where(sc => sc.StudentId == studentId && sc.CrsId == courseID).FirstOrDefaultAsync();
                //if (markedExam != null)
                //{
                //    db.StudentCourses.Remove(markedExam);

                //    await db.SaveChangesAsync();
                //}

                foreach (var answer in studentAnswers)
                {
                    //var questionGrade = await db.ExamQuestions.Where(eq => eq.ExamId == answer.ExamId && eq.QuestionId == answer.QuestionId).Select(eq => eq.Degree).FirstOrDefaultAsync(); //get the degree of the question
                    //if (answer.SelectedOption == 1)
                    //{
                    //    grade += questionGrade; //add the degree of the question to the total grade
                    //}
                    db.StudentAnswers.Add(answer);
                }

                await db.SaveChangesAsync();

                var sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@exam_id", examId),
                    new SqlParameter("@std_id", studentId)
                };

                await db.Database.ExecuteSqlRawAsync("EXECUTE CorrectExam @exam_id, @std_id", sqlParams);


                //StudentExam studentExam = new StudentExam
                //{
                //    ExamId = examId,
                //    StdId = studentId,
                //    ExamDate = DateOnly.FromDateTime(DateTime.Now),
                //    Grade = grade
                //};

                //db.StudentExams.Add(studentExam);

                //await db.SaveChangesAsync();



                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<StudentCourse> GetStudentCourseDegree(int examId, int studentId) //get the student exam degree
        {
            try
            {
                int CrsId = GetExamById(examId).Result.CrsId;
                return await db.StudentCourses.Where(se => se.CrsId == CrsId && se.StudentId == studentId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> IsStudentExamSubmitted(int examId, int studentId) //check if the student exam is submitted
        {
            try
            {
                int CrsId = GetExamById(examId).Result.CrsId;
                return await (db.StudentCourses.AnyAsync(se => se.CrsId == CrsId && se.StudentId == studentId && se.Grade != null));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<StudentCourse>> GetStudentCourses(int id) //get the student courses
        {
            try
            {
                //var parameters = new SqlParameter[]
                //{
                //    new SqlParameter("@stdId", id)
                //};

                //return await db.StudentCourses.FromSqlRaw("EXECUTE sp_Get_Cources_By_StudentId @stdId", parameters).ToListAsync();

                return await db.StudentCourses.Where(sc => sc.StudentId == id).Include(sc => sc.Crs).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<StudentCourse>> GetStudentResultsByStdId(int stdId)
        {
            try
            {
                return await db.StudentCourses.Where(sc => sc.StudentId == stdId).Include(sc => sc.Crs).Where(s => s.Grade != null).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<Exam> GetResultExam(int stdId, int crsId)
        {
            var StdExam = await db.StudentCourses.FirstOrDefaultAsync(se => se.StudentId == stdId);
            if (StdExam != null)
            {
                int examId = GetExamByCrsId(crsId).Result.ExamId;
                if (db.Exams.FirstOrDefault(s => s.ExamId == examId).CrsId == crsId)
                {
                    return await db.Exams.Where(e => e.ExamId == examId).Include(c => c.Crs).Include(e => e.ExamQuestions).ThenInclude(q => q.Question).ThenInclude(qo => qo.QuestionOptions).FirstOrDefaultAsync();
                }
                else { return null; }
            }
            else
            {
                return null;
            }
        }
        public async Task<List<string>> StudentAnswer(int examId, int studentId, List<int> questionId)
        {
            try
            {
                var answers = await db.StudentAnswers
                    .Where(sa => sa.ExamId == examId && sa.StudentId == studentId)
                    .ToListAsync();

                Dictionary<int, int> answerOp = answers.ToDictionary(sa => sa.QuestionId, sa => sa.SelectedOption);

                List<string> studentAnswers = new List<string>();
                foreach (int id in questionId)
                {
                    if (answerOp.ContainsKey(id))
                    {
                        int selectedOption = answerOp[id];
                        string answerText = await db.QuestionOptions
                            .Where(qo => qo.QuestionId == id && qo.OptionNo == selectedOption)
                            .Select(qo => qo.OptionText)
                            .FirstOrDefaultAsync();

                        studentAnswers.Add(answerText);
                    }
                    else
                    {
                        studentAnswers.Add("UnAnswered");
                    }
                }

                return studentAnswers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<List<string>> ExamQuestionOptions(List<ExamQuestion> examQuestion)
        {
            List<List<string>> myList = new List<List<string>>();
            foreach (var item in examQuestion)
            {
                var i = 1;
                List<string> options = new List<string>();
                foreach (var option in item.Question.QuestionOptions)
                {
                    if (option.OptionNo == 0) { i = 0; }
                    options.Add(item.Question.QuestionOptions.Where(qo => qo.QuestionId == item.QuestionId && qo.OptionNo == i).Select(qo => qo.OptionText).FirstOrDefault());
                    i++;
                }
                myList.Add(options);
            }
            return myList;
        }
        public List<string> ExamQuestionAnswers(List<ExamQuestion> examQuestion)
        {
            List<string> answers = new List<string>();
            Dictionary<int, int> answerData = new Dictionary<int, int>();
            foreach (var item in examQuestion)
            {
                answerData.Add(item.QuestionId, item.Question.QuestionAnswer);
            }
            foreach (var item in answerData)
            {
                answers.Add(db.QuestionOptions.Where(qo => qo.QuestionId == item.Key && qo.OptionNo == item.Value).Select(qo => qo.OptionText).FirstOrDefault());
            }
            return answers;
        }

        public async Task MarkExam(int examId, int studentId)
        {
            try
            {
                int CrsId = GetExamById(examId).Result.CrsId;
                var markedExam = db.StudentCourses.Where(sc => sc.CrsId == CrsId && sc.StudentId == studentId).FirstOrDefault();
                if (markedExam != null)
                {
                    markedExam.Grade = "0";

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<User> GetUserById(int id)
        {
			try
            {
				return await db.Users.Where(u => u.UserId == id).Include(s=>s.Student).ThenInclude(s=>s.StudentCourses).ThenInclude(s=>s.Crs).FirstOrDefaultAsync();
			}
			catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
				return null;
			}
        }
    }
}
