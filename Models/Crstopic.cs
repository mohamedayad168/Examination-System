using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class Crstopic
{
    public int TopicId { get; set; }

    public string TopicName { get; set; } = null!;

    public int? CrsId { get; set; }

    public virtual Course? Crs { get; set; }
}
