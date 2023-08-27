using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.Entities
{
    [Table("MenuType")]
    public partial class MenuType : EntityBase
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
