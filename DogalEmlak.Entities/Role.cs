using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogalEmlak.Entities
{
	public class Role : IEntity
	{
		[Key]
		public Guid Id { get; set; }

		[StringLength(20)]
		public string Authority { get; set; }
	}
}
