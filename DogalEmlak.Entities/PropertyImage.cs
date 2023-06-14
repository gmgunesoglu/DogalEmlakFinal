using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogalEmlak.Entities
{
	public class PropertyImage : IEntity
	{
		[Key]
		public Guid Id { get; set; }

		[ForeignKey("Property")]
		public Guid PropertyId { get; set; }

		public byte[] ImageData { get; set; }
	}
}
