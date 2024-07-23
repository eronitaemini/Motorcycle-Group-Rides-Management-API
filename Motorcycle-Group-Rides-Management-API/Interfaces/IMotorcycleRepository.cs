using System;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
	public interface IMotorcycleRepository
	{
		public List<Motorcycle> GetAll();
		public Motorcycle GetById(int id);
		public void Create(Motorcycle motorcycle);
		public void Delete(int id);
		public void Update(Motorcycle motorcycle);
		public bool SaveChanges();
	}
}

