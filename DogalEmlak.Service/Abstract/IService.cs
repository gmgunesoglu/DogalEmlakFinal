using DogalEmlak.Dao.Abstract;
using DogalEmlak.Entities;

namespace DogalEmlak.Service.Abstract
{
	public interface IService<T> : IRepository<T> where T : class, IEntity, new()
	{

	}
}
