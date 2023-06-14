using DogalEmlak.Dao.Concrete;
using DogalEmlak.Dao;
using DogalEmlak.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DogalEmlak.Service
{
    public class RoleService
    {
        private readonly DatabaseContext context;
        private readonly Repository<Role> repository;

        public RoleService()
        {
            context = new DatabaseContext();
            repository = new Repository<Role>(context);
        }

        public Role Get(string roleName)
        {
            return repository.Get(r => r.Authority == roleName);
        }
		public Role Get(Expression<Func<Role, bool>> expression)
		{
			return repository.Get(expression);
		}
	}
}
