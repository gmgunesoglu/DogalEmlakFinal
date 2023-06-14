using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Models
{
	public class UserUpdateModel
	{
		[Required(ErrorMessage = "Enter first name!")]
		[MinLength(3, ErrorMessage = "Min 3 characters!")]
		[MaxLength(30, ErrorMessage = "Max 30 characters!")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Enter last name")]
		[MinLength(3, ErrorMessage = "Min 3 characters!")]
		[MaxLength(20, ErrorMessage = "Max 20 characters!")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Enter password!")]
		[MinLength(8, ErrorMessage = "Min 8 characters!")]
		[MaxLength(20, ErrorMessage = "Max 20 characters!")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Enter mail address!")]
		[MinLength(10, ErrorMessage = "Min 10 characters!")]
		[MaxLength(100, ErrorMessage = "Max 100 character!")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Enter phone!")]
		[MinLength(10, ErrorMessage = "Min 10 characters!")]
		[MaxLength(14, ErrorMessage = "Max 14 character!")]
		public string Phone { get; set; }
	}
}
