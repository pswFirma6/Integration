using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Repository
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> table;

        public Repo(DatabaseContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public void Add(T newObject)
        {
            table.Add(newObject);
            Save();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Update(obj);
        }
    }
}
