using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Service.Exceptions
{
	public class UserLocked : Exception
	{
		public UserLocked() : base("This user is locked!")
		{
		}

		public UserLocked(string str) : base(str)
		{
		}
	}
}
