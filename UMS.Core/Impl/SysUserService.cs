
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Component.Data;
using UMS.Core.Data;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Core
{
    public partial class SysUserService
    {

        public ISysRightService RightService { get; set; }

        public SysUserService()
        {

        }

        public IEnumerable<SysUser> GetList()
        {
            return BaseRepository.Entities;
        }

        public IQueryable<SysUser> GetPagedList(GridPager pager)
        {
            var queryData= BaseRepository.Entities;
            pager.totalRows = queryData.Count();

            queryData = DataSorting(queryData, pager.sort, pager.order);

            return queryData.Skip((pager.page - 1) * pager.rows).Take(pager.rows);
        }

        public SysUser Login(string userName, string password)
        {
            return CurrentRepository.Login(userName, password);
        }

        public List<PermModel> GetPermission(string accountid, string controller)
        {
            return CurrentRepository.GetPermission(accountid, controller);
        }

        public IQueryable<RoleDTO> GetRoleByUserId(ref GridPager pager, string userId)
        {
            IQueryable<RoleDTO> queryData = CurrentRepository.GetRoleByUserId(userId);
            pager.totalRows = queryData.Count();
            
            queryData = DataSorting(queryData, pager.sort, pager.order); 

            return queryData.Skip((pager.page - 1) * pager.rows).Take(pager.rows);
        }

        public bool UpdateUserRole(string userId, string[] roleIds)
        { 
            CurrentRepository.UpdateUserRole(userId, roleIds);
            return true; 
        }

    }
}
