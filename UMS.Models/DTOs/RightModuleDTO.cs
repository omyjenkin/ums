using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.DTOs
{
    [NotMapped]
    public class RightModuleDTO: SysModuleOperate
    {
        public string RightId { get; set; }
    }
}
