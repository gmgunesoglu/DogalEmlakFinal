using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Service.Exceptions
{
	public class PropertyCouldnotAdded : Exception
	{
		public PropertyCouldnotAdded() : base("Property couldn't added!")
		{
		}

		public PropertyCouldnotAdded(string str) : base(str)
		{
		}
	}
}
