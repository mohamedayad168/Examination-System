using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public decimal? Salary { get; set; }

    public virtual ICollection<Crsdepin> Crsdepins { get; set; } = new List<Crsdepin>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual User InstructorNavigation { get; set; } = null!;

    public virtual ICollection<Department> DepNos { get; set; } = new List<Department>();
}
