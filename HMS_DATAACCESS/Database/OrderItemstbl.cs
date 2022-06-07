using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HMS_DATAACCESS.Database
{
    [Table("OrderItemstbl")]
    public partial class OrderItemstbl
    {
        [Key]
        public int Id { get; set; }
        public int? OrderId { get; set; }
        [StringLength(100)]
        public string ItemName { get; set; }
        public int? ItemId { get; set; }
        [Column("status")]
        [StringLength(100)]
        public string Status { get; set; }

        [ForeignKey(nameof(ItemId))]
        [InverseProperty(nameof(FoodItem.OrderItemstbls))]
        public virtual FoodItem Item { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(OrderTbl.OrderItemstbls))]
        public virtual OrderTbl Order { get; set; }
    }
}
