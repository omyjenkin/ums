using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Component.Data
{
    public interface IDbContext:IDisposable
    {
       DbContextConfiguration Configuration { get; }

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();

        void Update<TEntity, TKey>(params TEntity[] entities) where TEntity : class,new();

        void Update<TEntity, TKey>(Expression<Func<TEntity, object>> propertyExpression, params TEntity[] entities) where TEntity : class, new();

        int SaveChanges(bool validateOnSaveEnabled);
    }
}
