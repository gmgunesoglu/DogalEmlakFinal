using DogalEmlak.Dao.Abstract;
using DogalEmlak.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Dao.Concrete
{
	public class Repository<T> : IRepository<T> where T : class, IEntity, new()
	{


		internal DatabaseContext context;
		internal DbSet<T> dbSet;

		public Repository(DatabaseContext context)
		{
			this.context = context;
			dbSet = context.Set<T>();
		}


		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public async Task AddAsync(T entity)
		{
			await dbSet.AddAsync(entity);
		}

		public void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		public void DeleteAll(Expression<Func<T, bool>> expression)
		{
			List<T> entities = dbSet.Where(expression).ToList();
			foreach(T entity in entities)
			{
				dbSet.Remove(entity);
			}
		}

		public T Find(Guid id)
		{
			return dbSet.Find(id);
		}

		public async Task<T> findAsync(Guid id)
		{
			return await dbSet.FindAsync(id);
		}

		public T Get(Expression<Func<T, bool>> expression)
		{
			return dbSet.FirstOrDefault(expression);
		}

		public List<T> GetAll()
		{
			return dbSet.ToList();
		}

		public List<T> GetAll(Expression<Func<T, bool>> expression)
		{
			return dbSet.Where(expression).ToList();
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await dbSet.ToListAsync();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
		{
			return await dbSet.Where(expression).ToListAsync();
		}

		public Task<T> GetAsync(Expression<Func<T, bool>> expression)
		{
			return dbSet.FirstOrDefaultAsync(expression);
		}

		public int Save()
		{
			return context.SaveChanges();
		}

		public async Task<int> SaveAsync()
		{
			return await context.SaveChangesAsync();
		}

		public void Update(T entity)
		{
			context.Update(entity);
		}
	}
}
