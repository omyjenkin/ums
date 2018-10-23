using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using UMS.Models;
using UMS.Models.DTOs;

namespace UMS.Web
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<SysUser,UserDTO >(); 
            CreateMap<SysRightOperate, RightDTO>();
            CreateMap<SysModuleOperate, RightModuleDTO>();
        }
    }
}