using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class StudentCourse
{
    public string? Grade { get; set; }

    public int CrsId { get; set; }

    public int StudentId { get; set; }

    public virtual Course Crs { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
