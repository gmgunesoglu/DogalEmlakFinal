using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Models
{
	public class UserLoginModel
	{
		[Required(ErrorMessage = "Enter username!")]
		[MinLength(8, ErrorMessage = "Min 8 characters!")]
		[MaxLength(20, ErrorMessage = "Max 20 characters!")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Enter password!")]
		[MinLength(8, ErrorMessage = "Min 8 characters!")]
		[MaxLength(20, ErrorMessage = "Max 20 characters!")]
		public string Password { get; set; }
	}
}
