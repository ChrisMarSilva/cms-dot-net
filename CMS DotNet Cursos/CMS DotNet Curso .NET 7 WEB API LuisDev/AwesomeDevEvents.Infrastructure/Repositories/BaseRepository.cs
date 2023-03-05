using AwesomeDevEvents.Domain.Models;
using AwesomeDevEvents.Infrastructure.Persistence;
using AwesomeDevEvents.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeDevEvents.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public T Get(Guid Id)
        {
            return _entities.SingleOrDefault(c => c.Id == Id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _entities.Remove(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public bool SaveChangesAsync()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
