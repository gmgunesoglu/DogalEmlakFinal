using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Dao.Abstract
{
	public interface IRepository<T> where T : class
	{
		List<T> GetAll();

		List<T> GetAll(Expression<Func<T,bool>> expression);

		T Get(Expression<Func<T,bool>> expression);

		T Find(Guid id);

		void Add(T entity);

		void Delete(T entity);

		void DeleteAll(Expression<Func<T, bool>> expression);

		void Update(T entity);

		int Save();

		//Asenkron Metotlar...

		Task<T> findAsync(Guid id);

		Task<T> GetAsync(Expression<Func<T,bool>> expression);

		Task<List<T>> GetAllAsync();

		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);

		Task AddAsync (T entity);

		Task<int> SaveAsync();
	}
}
