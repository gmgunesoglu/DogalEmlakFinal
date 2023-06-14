using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogalEmlak.Entities
{
	public class Property : IEntity
	{
		[Key]
		public Guid Id { get; set; }

		[StringLength(30)]
		public string Header { get; set; }

		[StringLength(20)]
		public string PropertyType { get; set; }

		[StringLength(20)]
		public string SalaryType { get; set; }

		[ForeignKey("User")]
		public Guid SalerId { get; set; }

		public Decimal Price { get; set; }

		[StringLength(200)]
		public string Address { get; set; }

		[StringLength(5)]
		public string Room { get; set; }

		[StringLength(5)]
		public string NetSize { get; set; }

		[StringLength(5)]
		public string GrossSize { get; set; }

		public DateTime DateOfAdded { get; set; } = DateTime.Now;

		public DateTime DateOfRenewal { get; set; }

		public virtual List<PropertyImage>? PropertyImages { get; set; }
	}
}
