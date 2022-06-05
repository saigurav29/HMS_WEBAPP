using HMS_Repository.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Repository.Interface
{
    public interface IOrdersRepository
    {
        Task<IList<OrderDTO>> GetpendingOrders();
        Task<dashboardCards> getorderscount();
        Task<List<tabledata>> getTableData();
        Task<List<bookingorderDeatils>> getBookingTableorderDetails();
    }
}
