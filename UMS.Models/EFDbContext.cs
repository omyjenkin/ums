
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UMS.Models;


namespace UMS.Models
{
    /// <summary>
    ///     EF数据访问上下文
    /// </summary>
    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("DefaultConnection") { }

        public EFDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) { }

        public EFDbContext(DbConnection existingConnection)
            : base(existingConnection, true) { }

        //public IEnumerable<IEntityMapper> EntityMappers { get; set; }

        //new DbContextConfiguration Configuration { get { return base.Configuration; } set { } }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

        }

        public void Update<TEntity, TKey>(params TEntity[] entities) where TEntity : class, new()
        {
            if (entities == null) throw new ArgumentNullException("entities");

            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = base.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = base.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException)
                {
                    //TEntity oldEntity = dbSet.Find(entity.Id);
                    //base.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }

        public void Update<TEntity, TKey>(Expression<Func<TEntity, object>> propertyExpression, params TEntity[] entities)
            where TEntity : class, new()
        {
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            if (entities == null) throw new ArgumentNullException("entities");
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = base.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = base.Entry(entity);
                    entry.State = EntityState.Unchanged;
                    foreach (var memberInfo in memberInfos)
                    {
                        entry.Property(memberInfo.Name).IsModified = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    //TEntity originalEntity = dbSet.Local.Single(m => Equals(m.Id, entity.Id));
                    //System.Data.Entity.Core.Objects.ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
                    //System.Data.Entity.Core.Objects.ObjectStateEntry objectEntry = objectContext.ObjectStateManager.GetObjectStateEntry(originalEntity);
                    //objectEntry.ApplyCurrentValues(entity);
                    //objectEntry.ChangeState(EntityState.Unchanged);
                    //foreach (var memberInfo in memberInfos)
                    //{
                    //    objectEntry.SetModifiedProperty(memberInfo.Name);
                    //}
                }
            }
        }

        public int SaveChanges(bool validateOnSaveEnabled)
        {
            bool isReturn = base.Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                base.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                return SaveChanges();
            }
            finally
            {
                if (isReturn)
                {
                    base.Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }
    }
}