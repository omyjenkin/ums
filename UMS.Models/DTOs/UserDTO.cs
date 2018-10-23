using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;

namespace UMS.Models.DTOs
{
    [NotMapped]
    public class UserDTO:SysUser  
    {
        public string Flag { get; set; }
    }
}
