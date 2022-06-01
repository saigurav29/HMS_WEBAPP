using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HMS_DATAACCESS.Database
{
    [Table("LoginMaster")]
    public partial class LoginMaster
    {
        public LoginMaster()
        {
            OrderTbls = new HashSet<OrderTbl>();
            TableMasters = new HashSet<TableMaster>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(15)]
        public string Mobile { get; set; }
        [Column("username")]
        [StringLength(15)]
        public string Username { get; set; }
        [Column("password")]
        [StringLength(15)]
        public string Password { get; set; }
        [StringLength(50)]
        public string EmailId { get; set; }
        public int? Role { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JoiningDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public bool? Isactive { get; set; }

        [InverseProperty(nameof(OrderTbl.Employee))]
        public virtual ICollection<OrderTbl> OrderTbls { get; set; }
        [InverseProperty(nameof(TableMaster.Employee))]
        public virtual ICollection<TableMaster> TableMasters { get; set; }
    }
}
