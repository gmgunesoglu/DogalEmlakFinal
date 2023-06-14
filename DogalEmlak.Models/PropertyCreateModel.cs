using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Models
{
    public class PropertyCreateModel : PropertyUpdateModel
	{
		public Guid SalerId { get; set; }

		DateTime DateOfAdded { get; set; }
    }
}
