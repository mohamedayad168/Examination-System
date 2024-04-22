using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class ExamQuestion
{
    public int ExamId { get; set; }

    public int QuestionId { get; set; }

    public int Degree { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
