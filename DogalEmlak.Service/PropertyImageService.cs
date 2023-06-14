using DogalEmlak.Dao.Concrete;
using DogalEmlak.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogalEmlak.Entities;
using DogalEmlak.Models;
using Microsoft.AspNetCore.Http;
using DogalEmlak.Service.Exceptions;
using System.Linq.Expressions;

namespace DogalEmlak.Service
{
	public class PropertyImageService
	{
		private readonly DatabaseContext context;
		private readonly Repository<PropertyImage> repository;

		public PropertyImageService()
		{
			context = new DatabaseContext();
			repository = new Repository<PropertyImage>(context);
		}

		public async Task AddAllAsync(List<IFormFile> files, Guid propertyId)
		{
			foreach (IFormFile file in files)
			{
				var stream = new MemoryStream();
				await file.CopyToAsync(stream);
				PropertyImage propertyImage = new PropertyImage
				{
					Id = Guid.NewGuid(),
					ImageData = stream.ToArray(),
					PropertyId = propertyId,		
				};
				await repository.AddAsync(propertyImage);
				stream.Dispose();
			}
			await context.SaveChangesAsync();
		}

		public async Task<List<byte[]>> GetImagesAsync(Guid propertyId)
		{
			List<PropertyImage> propertyImages = await repository.GetAllAsync(p => p.PropertyId == propertyId);
			List<byte[]> images = new List<byte[]>();
			foreach (PropertyImage propertyImage in propertyImages) 
			{
				images.Add(propertyImage.ImageData);
			}
			return images;
		}

		public async Task DeleteAllAsync(Expression<Func<PropertyImage, bool>> expression)
		{
			repository.DeleteAll(expression);
			await context.SaveChangesAsync();
		}
    }
}
