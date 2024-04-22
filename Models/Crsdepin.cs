using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class Crsdepin
{
    public int DepId { get; set; }

    public int CrsId { get; set; }

    public int InstructorId { get; set; }

    public virtual Course Crs { get; set; } = null!;

    public virtual Department Dep { get; set; } = null!;

    public virtual Instructor Instructor { get; set; } = null!;
}
