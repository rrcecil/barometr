﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barometr.Data;
using Barometr.Models;

namespace Barometr.Infrastructure
{
    public class GenericRepository<T> : IDisposable where T: class
    {
        protected ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<T> List()
        {
            return _db.Set<T>();
        }

        public ApplicationUser GetUserByUsername(string userName)
        {
            return _db.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public ApplicationUser GetUserById(string Id)
        {
            return _db.Users.FirstOrDefault(u => u.Id == Id);
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
