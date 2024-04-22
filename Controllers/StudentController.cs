using Examination_System.Models;
using Examination_System.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        readonly IStudentRepo SRepo; //student repository
        readonly IUserRepo URepo; //user repository

        public StudentController(IStudentRepo _SRepo, IUserRepo _URepo) //constructor
        {
            SRepo = _SRepo;
            URepo = _URepo;
        }

        public async Task<IActionResult> Index()
        {
			var stdResults = await SRepo.GetStudentResultsByStdId(URepo.GetUserId(User));
            ViewBag.stdResults = stdResults;
			User std = SRepo.GetUserById(URepo.GetUserId(User)).Result;
			return View(std);
        }

        [HttpGet]
        public IActionResult Exam(int crsId)
        {
            Exam exam = SRepo.GetExamByCrsId(crsId).Result;
            if (exam != null) //check if the exam exists
            {
                return View(exam);
            }
            else
            {
                ViewBag.Message = "No Exam Found";
                return View();
            }
        }

        [HttpPost]
        [ActionName("exam")]
        public IActionResult TakeExam(int examId)
        {
            Exam exam = SRepo.GetExamById(examId).Result;

            int studentId = URepo.GetUserId(User);

            if (SRepo.IsStudentExamSubmitted(examId, studentId).Result) // check if the student submitted the exam
            {
                return RedirectToAction("Result", new { examId, studentId });
            }

            if (exam != null) //check if the exam exists
            {
                SRepo.MarkExam(examId, studentId);
                return View("TakeExam", exam);
            }
            else
            {
                ViewBag.Message = "No Exam Found";
                return View("TakeExam");
            }
        }

        public IActionResult Result(int examId, int studentId)
        {
            if (SRepo.IsStudentExamSubmitted(examId, studentId).Result) // check if the student submitted the exam
            {
                return View(SRepo.GetStudentCourseDegree(examId, studentId).Result);
            }
            else
            {
                ViewBag.Message = "Exam Not Submitted";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Result(int examId, Dictionary<int, int> studentAnswers)
        {
            List<StudentAnswer> studentAnswersList = new List<StudentAnswer>();
            int studentId = URepo.GetUserId(User);

            foreach (var item in studentAnswers)
            {
                studentAnswersList.Add(new StudentAnswer
                {
                    ExamId = examId,
                    QuestionId = item.Key,
                    StudentId = studentId,
                    SelectedOption = item.Value
                });
            }

            var result = SRepo.SubmitExam(examId, studentId, studentAnswersList).Result;

            if (result) //submit the exam
            {
                StudentCourse studentResult = SRepo.GetStudentCourseDegree(examId, studentId).Result;
                return View(studentResult);
            }
            else
            {
                ViewBag.Message = "Failed to Submit Exam";
                return View();
            }
        }

        public IActionResult Courses()
        {
            int userId = URepo.GetUserId(User);

            return View(SRepo.GetStudentCourses(userId).Result);

        }

        public async Task<IActionResult> Results()
        {
            List<StudentCourse> stdResults = await SRepo.GetStudentResultsByStdId(URepo.GetUserId(User));
            return View(stdResults);
        }
        public async Task<IActionResult> ResultDetails(int id, int crsId)
        {
            try
            {
                Exam exam = await SRepo.GetResultExam(id, crsId);
                List<ExamQuestion> examQuestions = [.. exam.ExamQuestions];
                List<string> questions = new List<string>();
                foreach (var item in examQuestions)
                {
                    questions.Add(item.Question.QuestionText);
                }
                List<int> questionsId = new List<int>();
                foreach (var item in examQuestions)
                {
                    questionsId.Add(item.Question.QuestionId);
                }
                
                ViewBag.questions = questions;
                ViewBag.options = SRepo.ExamQuestionOptions(examQuestions);
                ViewBag.answers = SRepo.ExamQuestionAnswers(examQuestions);
                ViewBag.StudentAnswers = SRepo.StudentAnswer(exam.ExamId, id, questionsId).Result;
                return View(exam);
            }
            catch (Exception e)
            {
                return View("Index");
            }
        }

    }
}
