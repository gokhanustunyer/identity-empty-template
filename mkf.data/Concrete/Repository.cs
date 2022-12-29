using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using mkf.data.Abstract;
using mkf.data.Context;
using mkf.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mkf.data.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        // Kullanilacak DbContext nesnesinin tanimi
        private readonly AppDbContext _context;

        // DbContext nesnesinin Constructor uzerinden talep edilmesi
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        // Table nesnesinin Generic BaseEntity'nin kendisi veya kendisinden
        // turetilmis nesneye ait tabloya set edilmesi
        public DbSet<T> Table => _context.Set<T>();

        // Uc durumlara karsi direkt Context'e erismek icin context'in acik birakilmasi
        public AppDbContext Context => _context;

        
        // Arayuzde tanimi yapilan fonksiyonlarin Generic olarak doldurulmasi
        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
            await Table.AddRangeAsync(model);
            return true;
        }

        public IQueryable<T> GetAll()
                => Table.AsQueryable();

        public async Task<T> GetByIdAsync(int id)
                => await Table.AsQueryable().FirstOrDefaultAsync(t => t.Id == id);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
                => await Table.AsQueryable().FirstOrDefaultAsync(method);

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
                => Table.AsQueryable().Where(method);

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            T? model = await Table.
                FirstOrDefaultAsync(data => data.Id == id);
            if (model != null)
                return Remove(model);
            else
                return false;
        }

        public bool RemoveRange(List<T> model)
        {
            Table.RemoveRange(model);
            return true;
        }

        public async Task<int> SaveAsync()
                => await _context.SaveChangesAsync();

        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
