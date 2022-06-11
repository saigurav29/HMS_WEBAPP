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
        public int? Price { get; set; }
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

        public IList<OrderDTO> orderDetails { get; set; }

    }

    public class FoodItemList
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string itemType { get; set; }
        public int? price { get; set; }
        public string itemDec { get; set; }
        public int? categoryId { get; set; }
        public string categoryName { get; set; }
        public int counter { get; set; }

    }

    public class placeOrderDTO
    {
        public int orderId { get; set; }

        public int itemID { get; set; }
        public string orderstatus { get; set; }
        public int tableID { get; set; }
        public string itemName { get; set; }
        public int? itemprice { get; set; }
        public int counter { get; set; }
        public int? employeID { get; set; }
        public string itemstatus { get; set; }

    }
}
