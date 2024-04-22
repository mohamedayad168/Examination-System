using System.ComponentModel.DataAnnotations;

namespace Examination_System.Models
{
	public class LoginViewModel
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string UserPass { get; set; }
	}
}
