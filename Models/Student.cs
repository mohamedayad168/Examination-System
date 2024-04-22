using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class Student
{
    public int StdId { get; set; }

    public virtual User Std { get; set; } = null!;

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
}
