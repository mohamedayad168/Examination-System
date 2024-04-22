using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class StudentExam
{
    public int ExamId { get; set; }

    public int StdId { get; set; }

    public int Grade { get; set; }

    public DateOnly ExamDate { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual Student Std { get; set; } = null!;
}
