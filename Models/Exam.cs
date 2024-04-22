using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public DateOnly GenerationDate { get; set; }

    public int CrsId { get; set; }

    public int Duration { get; set; }

    public virtual Course? Crs { get; set; }

    public virtual ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();

    public virtual ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();

}
