using HMS_DATAACCESS.Database;
using HMS_Repository.Interface;
using HMS_Repository.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Repository.Repository
{
    public class OrderRepository: IOrdersRepository
    {
        private readonly HMSDBDbContext _context;

        public OrderRepository(HMSDBDbContext context)
        {
            _context = context;
        }

        public async Task<IList<OrderDTO>> GetpendingOrders()
        {
            var ordt = await _context.OrderTbls.ToListAsync();
            var data = (from s in ordt
                        join i in _context.OrderItemstbls on s.Id equals i.OrderId
                        where s.Orderstatus == "P"
                        select new OrderDTO
                        {
                            OrderID = s.Id,
                            EmployeeID = s.EmployeeId,
                            TableID = s.TableId,
                            ItemName = i.ItemName,
                            Orderstatus = s.Orderstatus,
                            ItemStatus = i.Status,
                            OrderitemID = i.Id,
                            OrderTime = s.OrderTime
                        }).ToList();
            return data;

        }

        public async Task<dashboardCards> getorderscount()
        {
            var ordercount = (from o in _context.OrderTbls where o.OrderTime.Value.Month == DateTime.Today.Month &&
                              o.OrderTime.Value.Year == DateTime.Today.Year && o.Orderstatus == "C" select o).Count();

            var allEmp = (from e in _context.LoginMasters select e).Count();
            var loginEmp = (from e in _context.LoginMasters where e.Isactive == true select e).Count();

            var TodayCompletedOrd = (from o in _context.OrderTbls
                              where o.OrderTime.Value.Date == DateTime.Today.Date &&
 o.OrderTime.Value.Year == DateTime.Today.Year && o.Orderstatus == "C"
                              select o).Count();
            var TodayongoingOrd = (from o in _context.OrderTbls
                                     where o.OrderTime.Value.Date == DateTime.Today.Date &&
        o.OrderTime.Value.Year == DateTime.Today.Year && o.Orderstatus == "P"
                                     select o).Count();
            dashboardCards ds = new dashboardCards();
            ds.CurrenMonthSales = ordercount;
            ds.TotalEmp = allEmp;
            ds.loginEmp = loginEmp;
            ds.todaysongoingOrder = TodayongoingOrd;
            ds.todaysOrder = TodayCompletedOrd;
            return await Task.Run(()=>ds);
        }

        public async Task<List<tabledata>> getTableData()
        {
            var tbl = (from t in _context.TableMasters
                       select new tabledata
                       {
                           empid = t.EmployeeId,
                           Isactive = t.Isactive,
                           tableId = t.Id,
                           TableName = t.Name,
                           TableStatus = t.Status
                       }).ToListAsync();
            return await Task.Run(()=>tbl);
        }

        public async Task<List<bookingorderDeatils>> getBookingTableorderDetails()
        {
            var tbl = (from o in _context.OrderTbls join t in _context.TableMasters
                       on o.TableId equals t.Id where o.Orderstatus=="P"
                       select new bookingorderDeatils
                       {
                           orderID = o.Id,
                           Tablestatus = t.Status,
                           tableId = t.Id,
                           OrderStatus = o.Orderstatus,
                            Name = t.Name
                       }).ToListAsync();
            return await Task.Run(() => tbl);
        }
    }
}
