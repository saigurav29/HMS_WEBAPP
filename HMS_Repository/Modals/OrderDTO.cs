using System;
using System.Collections.Generic;
using System.Text;

namespace HMS_Repository.Modals
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public string Orderstatus { get; set; }
        public int? EmployeeID { get; set; }
        public int? TableID { get; set; }
        public DateTime? OrderTime { get; set; }
        public int? OrderitemID  { get; set; }
        public string ItemName { get; set; }
        public string ItemStatus { get; set; }
    }
    public class dashboardCards
    {
        public int CurrenMonthSales { get; set; }
        public int TotalEmp { get; set; }
        public int loginEmp { get; set; }
        public int todaysOrder { get; set; }
        public int todaysongoingOrder { get; set; }

    }

    public class tabledata
    {

        public int tableId { get; set; }
        public bool? Isactive { get; set; }
        public string TableName { get; set; }
        public int? empid { get; set; }
        public string TableStatus { get; set; }

    }
    public class bookingorderDeatils
    {
        public int orderID { get; set; }

        public int tableId { get; set; }
        public string OrderStatus { get; set; }
        public string Name { get; set; }
        public string Tablestatus { get; set; }


    }
}
