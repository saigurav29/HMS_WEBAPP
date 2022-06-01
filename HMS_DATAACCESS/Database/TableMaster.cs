using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HMS_DATAACCESS.Database
{
    [Table("TableMaster")]
    public partial class TableMaster
    {
        public TableMaster()
        {
            OrderTbls = new HashSet<OrderTbl>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public bool? Isactive { get; set; }
        public int? EmployeeId { get; set; }
        [StringLength(20)]
        public string Status { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(LoginMaster.TableMasters))]
        public virtual LoginMaster Employee { get; set; }
        [InverseProperty(nameof(OrderTbl.Table))]
        public virtual ICollection<OrderTbl> OrderTbls { get; set; }
    }
}
