using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Models
{
    public class PropertyUpdateModel : PropertyListModel
	{
        [Required(ErrorMessage = "Enter address!")]
        [MinLength(10, ErrorMessage = "Min 10 characters!")]
        [MaxLength(200, ErrorMessage = "Max 200 characters!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter room!")]
        [MaxLength(5, ErrorMessage = "Max 5 characters!")]
        public string Room { get; set; }

        [Required(ErrorMessage = "Enter net size!")]
        [MaxLength(5, ErrorMessage = "Max 5 characters!")]
        public string NetSize { get; set; }

        [Required(ErrorMessage = "Enter gross size size!")]
        [MaxLength(5, ErrorMessage = "Max 5 characters!")]
        public string GrossSize { get; set; }

		public List<IFormFile> Files { get; set; } = new List<IFormFile>();

		public List<byte[]>? Images { get; set; }
    }
}
