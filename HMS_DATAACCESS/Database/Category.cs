using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HMS_DATAACCESS.Database
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            FoodItems = new HashSet<FoodItem>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty(nameof(FoodItem.Category))]
        public virtual ICollection<FoodItem> FoodItems { get; set; }
    }
}
