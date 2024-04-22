using Examination_System.Repos;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Grid;

namespace Examination_System.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportsRepo reportsRepo;
        public ReportsController(IReportsRepo _reportsRepo)
        {
            reportsRepo = _reportsRepo;
        }
        public IActionResult Index()
        {
            return View("Reports");
        }
        public IActionResult GetInstCoursAndStdReport(int instId)
        {
            var data = reportsRepo.GetInstCoursAndStdReport(instId);
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //add image to the pdf
            FileStream imageStream = new FileStream("wwwroot/assets/iti-logo-text.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            SizeF imageSize = new SizeF(40, 40);
            PointF imageLocation = new PointF(14, 13);
            page.Graphics.DrawImage(image, imageLocation, imageSize);
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20,PdfFontStyle.Bold);
            //set the text coor
            //Draw the text.
            graphics.DrawString("ITIReports", font, PdfBrushes.DarkRed, new PointF(13,80));
            graphics.DrawString("Project SQL", font, PdfBrushes.DarkRed, new PointF(13, 105));


            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Saving the PDF to the MemoryStream.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Apply built-in table style
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(0, 300));
            //Write the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Set the position as '0'.
            stream.Position = 0;
            //Download the PDF document in the browser.
            //Close the document.
            document.Close(true);

            // Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "AllStudntAndCourses.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);


        }
        //////////student grades report

        public IActionResult GetStudentGradesReport(int stdId)
        {
            var data = reportsRepo.GetStudentGradesReport(stdId);
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            //Draw the text.
            graphics.DrawString("ITIReports", font, PdfBrushes.Black, new PointF(200, 300));
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Saving the PDF to the MemoryStream.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Apply built-in table style
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 10));
            //Write the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Set the position as '0'.
            stream.Position = 0;
            //Download the PDF document in the browser.
            //Close the document.
            document.Close(true);

            // Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "Grades.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);


        }
        //////////students by dept report////////

        public IActionResult GetStudentsByDeptNums(int deptNum)
        {
            var data = reportsRepo.GetStudentsByDeptNums(deptNum);
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //add image to the pdf
            FileStream imageStream = new FileStream("wwwroot/assets/iti-logo-text.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            SizeF imageSize = new SizeF(40, 40);
            PointF imageLocation = new PointF(14, 13);
            page.Graphics.DrawImage(image, imageLocation, imageSize);
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
            //set the text coor
            //Draw the text.
            graphics.DrawString("ITIReports", font, PdfBrushes.DarkRed, new PointF(13, 80));
            graphics.DrawString("Project SQL", font, PdfBrushes.DarkRed, new PointF(13, 105));


            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Saving the PDF to the MemoryStream.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Apply built-in table style
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(0, 300));
            //Write the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Set the position as '0'.
            stream.Position = 0;
            //Download the PDF document in the browser.
            //Close the document.
            document.Close(true);

            // Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "AllStudents.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);


        }
        //////////course topics report////////

        public IActionResult GetCourseTopics(int crsId)
        {
            var data = reportsRepo.GetCourseTopics(crsId);
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //add image to the pdf
            FileStream imageStream = new FileStream("wwwroot/assets/iti-logo-text.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            SizeF imageSize = new SizeF(40, 40);
            PointF imageLocation = new PointF(14, 13);
            page.Graphics.DrawImage(image, imageLocation, imageSize);
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
            //set the text coor
            //Draw the text.
            graphics.DrawString("ITIReports", font, PdfBrushes.DarkRed, new PointF(13, 80));
            graphics.DrawString("Topic Reports", font, PdfBrushes.DarkRed, new PointF(13, 105));
            graphics.DrawString($"Topics for Course Number {crsId}", font, PdfBrushes.DarkRed, new PointF(100, 15));


            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Saving the PDF to the MemoryStream.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Apply built-in table style
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(0, 300));
            //Write the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Set the position as '0'.
            stream.Position = 0;
            //Download the PDF document in the browser.
            //Close the document.
            document.Close(true);

            // Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "CourseTopics.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);


        }

        //////////exam questions with student answers report////////

        public IActionResult get_exam_questions_with_student_answers(int ExamId, int stdId)
        {
            var data = reportsRepo.get_exam_questions_with_student_answers(ExamId, stdId);
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //add image to the pdf
            FileStream imageStream = new FileStream("wwwroot/assets/iti-logo-text.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            SizeF imageSize = new SizeF(40, 40);
            PointF imageLocation = new PointF(14, 13);
            page.Graphics.DrawImage(image, imageLocation, imageSize);
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
            //set the text coor
            //Draw the text.
            graphics.DrawString("ITIReports", font, PdfBrushes.DarkRed, new PointF(13, 80));
            graphics.DrawString("Student Answers", font, PdfBrushes.DarkRed, new PointF(100, 105));
            graphics.DrawString($"Exam Number {ExamId} for Student {stdId}", font, PdfBrushes.DarkRed, new PointF(100, 15));

            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Saving the PDF to the MemoryStream.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Apply built-in table style
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(0, 300));
            //Write the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Set the position as '0'.
            stream.Position = 0;
            //Download the PDF document in the browser.
            //Close the document.
            document.Close(true);

            // Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "StudentAnswer.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);


        }
        //////////exam questions with student Choices report////////
        public IActionResult GetExamChoices(int ExamId)
        {
            var data = reportsRepo.GetExamChoices(ExamId);
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //add image to the pdf
            FileStream imageStream = new FileStream("wwwroot/assets/iti-logo-text.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            SizeF imageSize = new SizeF(40, 40);
            PointF imageLocation = new PointF(14, 13);
            page.Graphics.DrawImage(image, imageLocation, imageSize);
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
            //set the text coor
            //Draw the text.

            graphics.DrawString("ITIReports", font, PdfBrushes.DarkRed, new PointF(13, 80));
            graphics.DrawString("Exam Choices", font, PdfBrushes.DarkRed, new PointF(13, 105));

            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Saving the PDF to the MemoryStream.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Apply built-in table style
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(0, 300));
            //Write the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Set the position as '0'.
            stream.Position = 0;
            //Download the PDF document in the browser.
            //Close the document.
            document.Close(true);

            // Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "ExamChoices.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);


        }

    }
}
