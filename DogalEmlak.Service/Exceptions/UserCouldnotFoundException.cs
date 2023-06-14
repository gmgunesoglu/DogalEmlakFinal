using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Service.Exceptions
{
	public class UserCouldnotFoundException : Exception
	{
		public UserCouldnotFoundException() : base("User couldn't found!")
		{
		}

		public UserCouldnotFoundException(string str) : base(str)
		{
		}
	}
}
