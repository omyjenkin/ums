﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//	   如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//

//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using UMS.Models;


namespace UMS.Core
{
	/// <summary>
    ///   业务层接口——MisArticleCategory
    /// </summary>
    public partial interface IMisArticleCategoryService 
    { 
		IQueryable<MisArticleCategory> List();
		IQueryable<MisArticleCategory> List(ref GridPager pager,Expression<Func<MisArticleCategory, bool>> propertyExpression);
		int Insert(MisArticleCategory entity, bool isSave = true);
		int Insert(IEnumerable<MisArticleCategory> entities, bool isSave = true);
		int Delete(String id, bool isSave = true);
		int Delete(MisArticleCategory entity, bool isSave = true);
		int Delete(IEnumerable<MisArticleCategory> entities, bool isSave = true);
		int Delete(Expression<Func<MisArticleCategory, bool>> predicate, bool isSave = true);
		int Update(MisArticleCategory entity, bool isSave = true);
		int Update(Expression<Func<MisArticleCategory, object>> propertyExpression, MisArticleCategory entity, bool isSave = true);
		MisArticleCategory GetByKey(String key);
       
	}
}
