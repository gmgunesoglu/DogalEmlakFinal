using DogalEmlak.Dao.Concrete;
using DogalEmlak.Dao;
using DogalEmlak.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogalEmlak.Models;
using System.Net.Http;
using DogalEmlak.Service.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;

namespace DogalEmlak.Service
{
	public class PropertyService
	{
		private readonly DatabaseContext context;
		private readonly Repository<Property> repository;

		public PropertyService()
		{
			context = new DatabaseContext();
			repository = new Repository<Property>(context);
		}

		public async Task AddAsync(PropertyCreateModel model, Guid salerId)
		{
			Property property = new Property
			{
				Address = model.Address,
				DateOfAdded = DateTime.Now,
				DateOfRenewal = DateTime.Now,
				GrossSize = model.GrossSize,
				Header = model.Header,
				Id = Guid.NewGuid(),
				NetSize = model.NetSize,
				Price = model.Price,
				Room = model.Room,
				SalaryType = model.SalaryType,
				PropertyType = model.PropertyType,
				SalerId = salerId
				
			};
			model.Id = property.Id;		
			await repository.AddAsync(property);
			PropertyImageService service = new PropertyImageService();
			service.AddAllAsync(model.Files,model.Id);
			int result = await context.SaveChangesAsync();
			if (result == 0)
			{
				throw new PropertyCouldnotAdded();
			}
		}

		public async Task<List<PropertyListModel>> PropertyListModelsAsync(string strId)
		{
			Guid userId;
			Guid.TryParse(strId, out userId);
			List<Property> properties = await repository.GetAllAsync(p => p.SalerId == userId);
			List<PropertyListModel> models = new List<PropertyListModel>();
            foreach (Property property in properties)
            {
				models.Add(new PropertyListModel
				{
					DateOfRenewal = property.DateOfRenewal,
					Header = property.Header,
					Id = property.Id,
					Price= property.Price,
					PropertyType = property.PropertyType,
					SalaryType = property.SalaryType
				});
            }
			return models;
        }

		public async Task<List<PropertyListModel>> PropertyListModelsAsync()
		{
			List<Property> properties = await repository.GetAllAsync();
			List<PropertyListModel> models = new List<PropertyListModel>();
			foreach (Property property in properties)
			{
				models.Add(new PropertyListModel
				{
					DateOfRenewal = property.DateOfRenewal,
					Header = property.Header,
					Id = property.Id,
					Price = property.Price,
					PropertyType = property.PropertyType,
					SalaryType = property.SalaryType
				});
			}
			return models;
		}

		public async Task<PropertyUpdateModel> GetPropertyUpdateModelAsync(Guid propertyId)
		{
			Property property = repository.Get(p => p.Id == propertyId);
			PropertyImageService service = new PropertyImageService();
            List<byte[]> images = await service.GetImagesAsync(propertyId);
            PropertyUpdateModel model = new PropertyUpdateModel
            {
                Id = propertyId,
                Address = property.Address,
                DateOfRenewal = property.DateOfRenewal,
                Header = property.Header,
                GrossSize = property.GrossSize,
                NetSize = property.NetSize,
                Price = property.Price,
                SalaryType = property.SalaryType,
                PropertyType = property.PropertyType,
                Room = property.Room,
                Images = images
            };
			return model;
        }
		
		public async Task UpdateProperty(PropertyUpdateModel model)
		{
			//eski resimleri sil
			PropertyImageService service = new PropertyImageService();
			await service.DeleteAllAsync(pi => pi.PropertyId == model.Id);

			//yeni resimleri ekle
			await service.AddAllAsync(model.Files, model.Id);

			//property yi güncelle
			Property property = repository.Get(p => p.Id ==  model.Id);
			property.Address = model.Address;
			property.Price = model.Price;
			property.NetSize = model.NetSize;
			property.GrossSize = model.GrossSize;
			property.NetSize = model.NetSize;
			property.DateOfRenewal = DateTime.Now;
			property.Header = model.Header;
			property.Room = model.Room;
			property.SalaryType = model.SalaryType;
			property.PropertyType = model.PropertyType;

			//kaydet
			int result = await context.SaveChangesAsync();
			if (result == 0)
			{
				throw new PropertyException("Property couldn't updated!");
			}
		}

		public async Task RemoveAsync(Guid id)
		{
			//resimleri sil
			PropertyImageService service = new PropertyImageService();
			await service.DeleteAllAsync(pi => pi.PropertyId == id);

			//property yi sil
			Property property = repository.Get(p => p.Id == id);
			repository.Delete(property);
			context.SaveChanges();
		}

		public async Task<PropertyModel> GetPropertyModelAsync(Guid propertyId)
		{
			Property property = await repository.GetAsync(p => p.Id == propertyId);
			
			PropertyImageService imageService = new PropertyImageService();
			UserService userService = new UserService();
			User user = userService.Get(property.SalerId);
			PropertyModel model = new PropertyModel
			{
				Id= propertyId,
				Header= property.Header,
				PropertyType = property.PropertyType,
				SalaryType = property.SalaryType,
				SalerId = property.SalerId,
				Price = property.Price,
				Address = property.Address,
				Room = property.Room,
				NetSize = property.NetSize,
				GrossSize = property.GrossSize,
				DateOfRenewal = property.DateOfRenewal,
				DateOfAdded = property.DateOfAdded,
				Images = await imageService.GetImagesAsync(propertyId),
				Phone = user.Phone
			};
			return model;
		}

		public async Task<int> GetCountOfPropertiesAsync(Guid salerId)
		{
			List<Property> properties = await repository.GetAllAsync(p => p.SalerId == salerId);
			return properties.Count();
		}

	}
}
