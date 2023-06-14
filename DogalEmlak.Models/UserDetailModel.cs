using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Models
{
    public class UserDetailModel : UserUpdateModel
    {
        public string UserName { get; set; }

        public Guid Id { get; set; }

        public string Role { get; set; }

        public bool Locked { get; set; }

        public DateTime DateOfAdded { get; set; }

        public int PropertyCount { get; set; }
    }
}
