
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Component.Data
{
    /// <summary>
    ///     定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    /// <typeparam name="TKey">实体主键类型</typeparam>
    public interface IRepository<TEntity, in TKey> : IDependency where TEntity : class, new()
    {
        #region 属性

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        #endregion

        #region 公共方法

        /// <summary>
        /// 根据条件表达式返回可查询的记录源
        /// </summary>
        /// <param name="match">查询条件</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> match, string sortPropertyName, bool isDescending = true);


        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(TEntity entity, bool isSave = true);

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(IEnumerable<TEntity> entities, bool isSave = true);

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(TKey id, bool isSave = true);

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(TEntity entity, bool isSave = true);

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(IEnumerable<TEntity> entities, bool isSave = true);

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true);

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Update(TEntity entity, bool isSave = true);

        /// <summary>
        /// 使用附带新值的实体信息更新指定实体属性的值
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="isSave">是否执行保存</param>
        /// <param name="entity">附带新值的实体信息，必须包含主键</param>
        /// <returns>操作影响的行数</returns>
        int Update(Expression<Func<TEntity, object>> propertyExpression, TEntity entity, bool isSave = true);

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        TEntity GetByKey(TKey key);

        /// <summary>
        /// 插入指定实体（异步）
        /// </summary>
        /// <param name="entity">指定的对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// 更新指定实体（异步）
        /// </summary>
        /// <param name="entity">指定的对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象（异步）
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        Task<int> DeleteByConditionAsync(Expression<Func<TEntity, bool>> match);

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
       int ExecSql(string sql, params object[] parameters);

        IQueryable<T> QuerySql<T>(string sql, params object[] parameters);

        IQueryable<dynamic> DynamicQuery<dynamic>(string sql, params object[] parameters);

        #endregion
    }
}