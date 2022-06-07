using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HMS_DATAACCESS.Database
{
    [Table("orderTbl")]
    public partial class OrderTbl
    {
        public OrderTbl()
        {
            OrderItemstbls = new HashSet<OrderItemstbl>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Orderstatus { get; set; }
        public int? EmployeeId { get; set; }
        [Column("TableID")]
        public int? TableId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OrderTime { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(LoginMaster.OrderTbls))]
        public virtual LoginMaster Employee { get; set; }
        [ForeignKey(nameof(TableId))]
        [InverseProperty(nameof(TableMaster.OrderTbls))]
        public virtual TableMaster Table { get; set; }
        [InverseProperty(nameof(OrderItemstbl.Order))]
        public virtual ICollection<OrderItemstbl> OrderItemstbls { get; set; }
    }
}
