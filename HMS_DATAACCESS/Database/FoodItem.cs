using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HMS_DATAACCESS.Database
{
    [Table("FoodItem")]
    public partial class FoodItem
    {
        public FoodItem()
        {
            OrderItemstbls = new HashSet<OrderItemstbl>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string ItemName { get; set; }
        [StringLength(100)]
        public string ItemType { get; set; }
        public int? Price { get; set; }
        [StringLength(200)]
        public string ItemDec { get; set; }
        [Column("categoryID")]
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("FoodItems")]
        public virtual Category Category { get; set; }
        [InverseProperty(nameof(OrderItemstbl.Item))]
        public virtual ICollection<OrderItemstbl> OrderItemstbls { get; set; }
    }
}
