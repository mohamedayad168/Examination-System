using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examination_System.Data;
using Examination_System.Models;
using Examination_System.Repos;
using Microsoft.AspNetCore.Authorization;
using Examination_System.ModelViews;

namespace Examination_System.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepo instructorRepo;
        private readonly IUserRepo userRepo;

        public InstructorController(IInstructorRepo _instructorRepo, IUserRepo _userRepo)
        {
            instructorRepo = _instructorRepo;
            userRepo = _userRepo;
        }

        [HttpGet]
        public IActionResult AddQuestion()
        {

            // get the courses for this signed in instructor and send them to the view

            var courses = instructorRepo.GetInstructorCourses(userRepo.GetUserId(User)).Result;
            ViewBag.courses = new SelectList(courses, "CrsId", "CrsName");

            return View(new Question());
        }

        [HttpPost]
        public IActionResult AddQuestion(IFormCollection form, Dictionary<int, string> options = null)
        {


            // get the question data from the form
            var question = new Question
            {
                QuestionText = form["QuestionText"],
                QuestionType = form["QuestionType"],
                CrsId = int.Parse(form["CrsId"]),
                QuestionAnswer = int.Parse(form["QuestionAnswer"])
            };

            //check if the question text is empty
            if (string.IsNullOrEmpty(question.QuestionText))
            {
                ViewBag.courses =
                    new SelectList(instructorRepo.GetInstructorCourses(userRepo.GetUserId(User)).Result, "CrsId",
                                               "CrsName"); //change with the signed in instructor id
                ModelState.AddModelError("", "Please fill the question text");
                return View(question);
            }

            //check if the question is MCQ and the options are filled

            foreach (var option in options)
            {
                if ( question.QuestionType == "MCQ" && string.IsNullOrEmpty(option.Value))
                {
                    ViewBag.courses =
                        new SelectList(instructorRepo.GetInstructorCourses(userRepo.GetUserId(User)).Result,
                            "CrsId",
                            "CrsName"); //change with the signed in instructor id
                    ModelState.AddModelError("", "Please fill all options");
                    return View(question);
                }
            }

            // add the question to the database
            var questionId = instructorRepo.AddQuestionToCourse(question).Result;
            if (questionId == -1)
            {
                ViewBag.courses =
                    new SelectList(instructorRepo.GetInstructorCourses(userRepo.GetUserId(User)).Result, "CrsId",
                        "CrsName");
                ModelState.AddModelError("", "Error adding question to the course");
                return View(question);


            }

            if (question.QuestionType == "MCQ")
            {
                if (!instructorRepo.AddQuestionOption(questionId, options).Result)
                {
                    ViewBag.courses =
                        new SelectList(instructorRepo.GetInstructorCourses(userRepo.GetUserId(User)).Result,
                            "CrsId",
                            "CrsName");
                    ModelState.AddModelError("", "Error adding question options");

                    return View(question);
                }

            }

            ViewBag.courses =
                new SelectList(instructorRepo.GetInstructorCourses(userRepo.GetUserId(User)).Result, "CrsId",
                                       "CrsName");
            ViewBag.success = "Question added successfully";
            return View(new Question());


        }




        [HttpGet]
        public IActionResult GenerateRandomExam()
        {
            // get the courses for this signed in instructor and send them to the view
            var courses = instructorRepo.GetInstructorCourses(userRepo.GetUserId(User)).Result;
            ViewBag.courses = new SelectList(courses, "CrsId", "CrsName");
            return View(new Exam());
        }

        [HttpPost]
        public async Task<IActionResult> GenerateRandomExam(Exam exam, int MCQCount, int TFCount, int degreeOfMCQ, int degreeOfTF)
        {
            try
            {

                var generatedExamId = await instructorRepo.GenerateRandomExam(exam, MCQCount, TFCount, degreeOfMCQ, degreeOfTF);
                ViewBag.success = "Exam Generated successfully";

                return RedirectToAction("GenerateRandomExam");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                Console.WriteLine(e);
                return View();
                //throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ShowResults()
        {

            var courses = await instructorRepo.GetInstructorCourses(userRepo.GetUserId(User));

            ViewBag.courses = new SelectList(courses, "CrsId", "CrsName");

            return View();
        }

        public async Task<IActionResult> GetStudentsResultByCourse(int crsId)
        {

            var students = await instructorRepo.GetStudentsResultByCourse(crsId);
            return PartialView(students);
        }

        public async Task<IActionResult> GenerateReports()
        {
            var students = await instructorRepo.GetStudents();
            var Instructors = await instructorRepo.GetInstructors();
            var Departments = await instructorRepo.GetDepartments();
            var Courses = await instructorRepo.GetCourses();
            var studentExams = await instructorRepo.GetExams();
            var Exams = await instructorRepo.Exams();
            var ExamQuestions = await instructorRepo.ExamQuestions();
            ViewBag.students = students;
            ViewBag.instructors = Instructors;
            ViewBag.departments = Departments;
            ViewBag.courses = Courses;
            ViewBag.studentExams = studentExams;
            ViewBag.exams = Exams;
            ViewBag.examQuestions = ExamQuestions;
            return View();
        }
        public async Task<IActionResult> EnrollStudent()
        {
            StudentCourse model = new StudentCourse();

            var allStudents = await instructorRepo.GetStudents();
            var allCourses = await instructorRepo.GetCourses();
            ViewBag.Students = new SelectList(allStudents, "UserId", "UserName");
            ViewBag.Courses = new SelectList(allCourses, "CrsId", "CrsName");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollStudent(EnrollmentModel model)
        {
            if (ModelState.IsValid)
            {
                var enrollmentSuccess = await instructorRepo.EnrollStudent(model.StudentId, model.CrsId);
                if (enrollmentSuccess)
                {
                    return RedirectToAction("ShowResults");
                }
                else
                {
                    ModelState.AddModelError("", "Student is already enrolled in this course.");
                }
            }

            ViewBag.Students = new SelectList(await instructorRepo.GetStudents(), "UserId", "UserName");
            ViewBag.Courses = new SelectList(await instructorRepo.GetCourses(), "CrsId", "CrsName");
            return View(model);
        }

        [HttpGet]
        //API to check if student is enrolled in a course
        public async Task<IActionResult> IsStudentEnrolled(int studentId, int courseId)
        {
            bool isEnrolled = await instructorRepo.IsStudentEnrolled(studentId, courseId);
            return Json(new { isEnrolled });
        }
        [HttpPost]

        public async Task<IActionResult> DeleteStudentFromCourse(int studentId, int courseId)
        {
            var removedAnsSuccessfully = await instructorRepo.RemoveStudentFromCourseAndAnswers(studentId, courseId);
            if (!removedAnsSuccessfully)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(ShowResults)); // Adjust as necessary
        }

    }
}

