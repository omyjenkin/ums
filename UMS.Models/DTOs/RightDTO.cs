using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.DTOs
{
    [NotMapped]
   public class RightDTO:SysRightOperate
    {
        public string Name { get; set; }

        public int Sort { get; set; }
    }
}
