using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models
{
   public partial class SysRole
    {
        [NotMapped]
        public string Flag { get; set; }
    }
}
