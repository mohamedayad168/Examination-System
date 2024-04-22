using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class Department
{
    public int DepId { get; set; }

    public string DepName { get; set; } = null!;

    public string? DepDescription { get; set; }

    public int? BrNo { get; set; }

    public int? MgrNo { get; set; }

    public virtual Branch? BrNoNavigation { get; set; }

    public virtual ICollection<Crsdepin> Crsdepins { get; set; } = new List<Crsdepin>();

    public virtual Instructor? MgrNoNavigation { get; set; }

    public virtual ICollection<Instructor> InsNos { get; set; } = new List<Instructor>();
}
