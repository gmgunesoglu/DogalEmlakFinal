using DogalEmlak.Dao;
using DogalEmlak.Dao.Concrete;
using DogalEmlak.Entities;
using DogalEmlak.Models;
using DogalEmlak.Service.Abstract;
using DogalEmlak.Service.Concrete;
using DogalEmlak.Service.Exceptions;
using NETCore.Encrypt.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DogalEmlak.Service
{
    public class UserService
    {
        private readonly DatabaseContext context;
        private readonly Repository<User> repository;

        public UserService()
        {
            context = new DatabaseContext();
            repository = new Repository<User>(context);
        }

        public void Add(UserRegisterModel model, string roleName)
        {
            if (repository.Get(u => u.UserName == model.UserName) == null)
            {
                Role role = new RoleService().Get(r => r.Authority == roleName);

                User user = new User
                {
                    DateOfAdded = DateTime.Now,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Id = Guid.NewGuid(),
                    Locked = false,
                    Password = model.Password.MD5(),
                    Phone = model.Phone,
                    UserName = model.UserName,
                    RoleId = role.Id
                };
                repository.Add(user);
                int reusult = context.SaveChanges();
                Console.WriteLine("user add: " + reusult);
            }
            else
            {
                throw new UsernameAlreadyUsedException("User name \"" + model.UserName + "\" is allready used!");
            }
        }

        public User GetLogin(UserLoginModel model)
        {
            model.Password = model.Password.MD5();
            User user = repository.Get(u => u.UserName == model.UserName && u.Password == model.Password);
            if (user == null)
            {
                throw new UserCouldnotFoundException();
            }
            if(user.Locked == true)
            {
                throw new UserLocked();
            }
			user.Role = new RoleService().Get(r => r.Id == user.RoleId);
			return user;
		}

        public UserUpdateModel GetUserUpdateModel(string strId)
        {
            Guid userId;
            Guid.TryParse(strId, out userId);
            User user = repository.Get(u => u.Id == userId);
            return new UserUpdateModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Phone = user.Phone
            };
        }

        public User Update(UserUpdateModel model, string strId)
        {
            Guid userId;
            Guid.TryParse(strId, out userId);
            User user = repository.Get(u => u.Id == userId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Password = model.Password.MD5();
            repository.Update(user);
            context.SaveChanges();
            user.Role = new RoleService().Get(r => r.Id == user.RoleId);
            return user;
        }

        public void Delete(string strId) 
        {
            Guid userId;
            Guid.TryParse(strId, out userId);
            User user = repository.Get(u => u.Id == userId);
            //repository.Delete(user);
            user.Locked = true;
            repository.Update(user);
            context.SaveChanges();
        }


        public async Task<List<UserListModel>> GetUsersInRoleAsync(string roleName)
        {
            List<UserListModel> models = new List<UserListModel>();
            RoleService roleService = new RoleService();
            Role role = roleService.Get(roleName);
            List<User> users = await repository.GetAllAsync(u => u.RoleId == role.Id);
            foreach (User user in users)
            {
                models.Add(new UserListModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = user.Password,
                    Locked = user.Locked,
                    UserName = user.UserName,
                });
            }
            return models;
        }

        public void Block(Guid userId)
        {
            User user = repository.Find(userId);
            user.Locked = true;
            repository.Update(user);
            context.SaveChanges();
        }

        public void UnBlock(Guid userId)
        {
            User user = repository.Find(userId);
            user.Locked = false;
            repository.Update(user);
            context.SaveChanges();
        }

        public User Get(Guid id)
        {
            return repository.Find(id);
        }

        public async Task<UserDetailModel> GetUserDetailModelAsync(Guid id)
        {
            User user = repository.Find(id);
            RoleService roelService = new RoleService();
            PropertyService propertyService = new PropertyService();
            int propertyCount = await propertyService.GetCountOfPropertiesAsync(id);
            Role role = roelService.Get(r => r.Id == user.RoleId);

            UserDetailModel model = new UserDetailModel
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                Role = role.Authority,
                Email = user.Email,
                Phone = user.Phone,
                Locked = user.Locked,
                DateOfAdded = user.DateOfAdded,
                PropertyCount = propertyCount
            };
            return model;
        }

        public async Task<UserDetailModel> GetUserDetailModelAsync(string strId)
        {
            Guid id;
            Guid.TryParse(strId, out id);
            User user = repository.Find(id);
            RoleService roelService = new RoleService();
            PropertyService propertyService = new PropertyService();
            int propertyCount = await propertyService.GetCountOfPropertiesAsync(id);
            Role role = roelService.Get(r => r.Id == user.RoleId);

            UserDetailModel model = new UserDetailModel
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                Role = role.Authority,
                Email = user.Email,
                Phone = user.Phone,
                Locked = user.Locked,
                DateOfAdded = user.DateOfAdded,
                PropertyCount = propertyCount
            };
            return model;
        }
    }
}
