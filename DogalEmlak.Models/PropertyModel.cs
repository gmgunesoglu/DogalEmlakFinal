using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Models
{
	public class PropertyModel : PropertyCreateModel
	{
		public string Phone { get; set; }

		public DateTime DateOfAdded { get; set; }
    }
}
