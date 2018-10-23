using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace UMS.Models
{
   public partial class SysModule
    {
        [NotMapped]
        public string state { get; set; } 
    }
}
