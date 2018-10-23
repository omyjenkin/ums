using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UMS.Component.Data;
using UMS.Models;
using UMS.Utility;

namespace UMS.Core
{
    public abstract class BaseService<TEntity, TKey> where TEntity : class, new()
    {
        public virtual IRepository<TEntity, TKey> BaseRepository { get; set; }

        #region 公共方法

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> List()
        {
            return BaseRepository.Entities;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> List(ref GridPager pager, Expression<Func<TEntity, bool>> propertyExpression)
        {
            var entities = BaseRepository.Entities;
            if (propertyExpression != null)
            {
                entities = entities.Where(propertyExpression);
            }
            
            entities = DataSorting(entities,pager.sort,pager.order);

            pager.totalRows = entities.Count();
            return entities.Skip((pager.page - 1) * pager.rows).Take(pager.rows);
        }


        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(TEntity entity, bool isSave = true)
        {
            return BaseRepository.Insert(entity, isSave);

        }

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            return BaseRepository.Insert(entities, isSave);
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TKey id, bool isSave = true)
        {
            return BaseRepository.Delete(id, isSave);
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TEntity entity, bool isSave = true)
        {
            return BaseRepository.Delete(entity, isSave);
        }

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            return BaseRepository.Delete(entities, isSave);
        }

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            return BaseRepository.Delete(predicate, isSave);
        }

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Update(TEntity entity, bool isSave = true)
        {
            return BaseRepository.Update(entity, isSave);
        }

        /// <summary>
        /// 使用附带新值的实体信息更新指定实体属性的值
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="isSave">是否执行保存</param>
        /// <param name="entity">附带新值的实体信息，必须包含主键</param>
        /// <returns>操作影响的行数</returns>
        public int Update(Expression<Func<TEntity, object>> propertyExpression, TEntity entity, bool isSave = true)
        {
            throw new NotSupportedException("上下文公用，不支持按需更新功能。");
            return BaseRepository.Update(propertyExpression, entity, isSave);

        }

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TEntity GetByKey(TKey key)
        {
            return BaseRepository.GetByKey(key);
        }


        #endregion

        #region 分页

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static IQueryable<T> DataSorting<T>(IQueryable<T> source, string sortExpression, string sortDirection)
        {
            string sortingDir = string.Empty;
            if (sortDirection.ToUpper().Trim() == "ASC")
                sortingDir = "OrderBy";
            else if (sortDirection.ToUpper().Trim() == "DESC")
                sortingDir = "OrderByDescending";
            ParameterExpression param = Expression.Parameter(typeof(T), sortExpression);
            PropertyInfo pi = typeof(T).GetProperty(sortExpression);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, sortExpression), param));
            IQueryable<T> query = source.AsQueryable().Provider.CreateQuery<T>(expr);
            return query;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> DataPaging<T>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber <= 1)
            {
                return source.Take(pageSize);
            }
            else
            {
                return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
        }

        /// <summary>
        /// 排序并分页 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> SortingAndPaging<T>(IQueryable<T> source, string sortExpression, string sortDirection, int pageNumber, int pageSize)
        {
            IQueryable<T> query = DataSorting<T>(source, sortExpression, sortDirection);
            return DataPaging(query, pageNumber, pageSize);
        }

        #endregion
    }
}
