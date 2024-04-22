using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class Branch
{
    public int BrId { get; set; }

    public string BrName { get; set; } = null!;

    public string? BrDescription { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
