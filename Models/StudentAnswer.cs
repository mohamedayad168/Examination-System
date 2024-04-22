using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class StudentAnswer
{
    public int ExamId { get; set; }

    public int QuestionId { get; set; }

    public int StudentId { get; set; }

    public int SelectedOption { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
    //public DateOnly ExamDate { get; set; }

    public override string ToString()
	{
        return $"ExamId: {ExamId}, QuestionId: {QuestionId}, StudentId: {StudentId}, SelectedOption: {SelectedOption}";
	}
}
