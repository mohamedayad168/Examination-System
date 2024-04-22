using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class Course
{
    public int CrsId { get; set; }

    public string CrsName { get; set; } = null!;

    public int CrsGrade { get; set; }

    public virtual ICollection<Crsdepin> Crsdepins { get; set; } = new List<Crsdepin>();

    public virtual ICollection<Crstopic> Crstopics { get; set; } = new List<Crstopic>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
