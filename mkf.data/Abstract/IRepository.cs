using Microsoft.EntityFrameworkCore;
using mkf.data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mkf.data.Abstract
{
    public interface IRepository<T> where T : class
    {
        // Okuma ile ilgili methodlar

        // Tum veriler
        IQueryable<T> GetAll();

        // Uygulanan fonsiyon sonucunda true donen tum veriler
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method);

        // Uygulanan fonksiyon sonucunda true donen tek veri
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method);

        // Integer tipinde verilen 'id' parametresiyle veri
        Task<T> GetByIdAsync(int id);


        ////////////////////////////////////////

        // Yazma ile ilgili methodlar

        // Asenkron olarak veri ekleme
        Task<bool> AddAsync(T model);

        // Asenktron olarak toplu veri ekleme
        Task<bool> AddRangeAsync(List<T> model);
        
        // Senkron veri silme
        bool Remove(T model);

        // Asenkron veri silme
        Task<bool> RemoveAsync(int id);

        // Asenkron toplu veri silme
        bool RemoveRange(List<T> model);

        // Senkron veri guncelleme
        bool Update(T model);

        // Track edilen verileri asenkron guncelleme, kayit etme
        Task<int> SaveAsync();

        // Context Tablosuna ulasma
        DbSet<T> Table { get; }

        // Context'in bizzat kendisine ulasma
        AppDbContext Context { get; }
    }
}
