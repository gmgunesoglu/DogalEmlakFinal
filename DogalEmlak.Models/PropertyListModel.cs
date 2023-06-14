using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Models
{
	public class PropertyListModel
	{
		public Guid Id { get; set; }    //id değişmez ama erişimi kolaylaştırır

		[Required(ErrorMessage = "Enter header name!")]
		[MinLength(3, ErrorMessage = "Min 3 characters!")]
		[MaxLength(30, ErrorMessage = "Max 30 characters!")]
		public string Header { get; set; }

		[Required(ErrorMessage = "Enter property type name!")]
		[MinLength(3, ErrorMessage = "Min 3 characters!")]
		[MaxLength(20, ErrorMessage = "Max 20 characters!")]
		public string PropertyType { get; set; }

		[Required(ErrorMessage = "Enter salary type name!")]
		[MinLength(3, ErrorMessage = "Min 3 characters!")]
		[MaxLength(20, ErrorMessage = "Max 20 characters!")]
		public string SalaryType { get; set; }

		public decimal Price { get; set; }

		public DateTime DateOfRenewal { get; set; }
	}
}
