using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Service.Exceptions
{
	public class PropertyException : Exception
	{
		public PropertyException(string str) : base(str)
		{
		}
	}
}
