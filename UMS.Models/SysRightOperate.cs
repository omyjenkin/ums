//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UMS.Models
{
    
    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// SysRightOperate
    /// </summary>
    public partial class SysRightOperate:EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
    	[Display(Name="")][Key]
        public string Id { get; set; }
    	   
        
        /// <summary>
        /// 
        /// </summary>
    	[Display(Name="")]
        public string RightId { get; set; }
    	   
        
        /// <summary>
        /// 
        /// </summary>
    	[Display(Name="")]
        public string KeyCode { get; set; }
    	   
        
        /// <summary>
        /// 
        /// </summary>
    	[Display(Name="")]
        public bool IsValid { get; set; }
    	   
        
    }
}
