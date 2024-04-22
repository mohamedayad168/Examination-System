using System;
using System.Collections.Generic;

namespace Examination_System.Models;

public partial class QuestionOption
{
    public int QuestionId { get; set; }

    public int OptionNo { get; set; }

    public string OptionText { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
