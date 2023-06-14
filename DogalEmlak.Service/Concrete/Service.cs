using DogalEmlak.Dao;
using DogalEmlak.Dao.Concrete;
using DogalEmlak.Entities;
using DogalEmlak.Service.Abstract;

namespace DogalEmlak.Service.Concrete
{
	public class Service<T> : Repository<T> , IService<T> where T : class, IEntity, new()
	{
		public Service(DatabaseContext context) : base(context)
		{
		}
	}
}
