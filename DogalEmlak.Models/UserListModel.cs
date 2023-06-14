using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Models
{
    public class UserListModel : UserUpdateModel
    {
        public Guid Id { get; set; }

        public bool Locked { get; set; }

        public string UserName { get; set; }
    }
}
