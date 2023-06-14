using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogalEmlak.Entities
{
	public class User : IEntity
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[StringLength(30)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(20)]
		public string LastName { get; set; }

		[ForeignKey("Role")]
		public Guid RoleId { get; set; }

		[Required]
		[StringLength(20)]
		public string UserName { get; set; }

		[Required]
		[StringLength(100)]
		public string Password { get; set; }

		[Required]
		[StringLength(100)]
		public string Email { get; set; }

		[Required]
		[StringLength(14)]
		public string Phone { get; set; }

		public bool Locked { get; set; } = false;

		public DateTime DateOfAdded { get; set; } = DateTime.Now;

		public virtual Role? Role { get; set; }
	}
}
