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
                       }).ToList();

            for (int i = 0; i < tbl.Count; i++)
            {
                tbl[i].orderDetails = await GetpendingOrdersByID(tbl[i].orderID);
            }
            return await Task.Run(() => tbl);
        }
        public async Task<IList<OrderDTO>> GetpendingOrdersByID(int orderID)
        {
            
            var data = (from s in _context.OrderItemstbls
                        join f in _context.FoodItems on 
                        s.ItemId equals f.Id
                        where s.OrderId == orderID
                        select new OrderDTO
                        {
                            OrderID = s.Id,
                            ItemName = s.ItemName,
                            ItemStatus = s.Status,
                            Price = f.Price
                           
                        }).ToList();
            return await Task.Run(()=>data);

        }

        public async Task<IList<FoodItemList>> getfooditemsList()
        {
            var fooditem = await _context.FoodItems.ToListAsync();
            var foodList = (from f in fooditem
                            join c in _context.Categories on f.CategoryId equals c.Id
                            select new FoodItemList
                            {
                                itemId = f.Id,
                                itemName = f.ItemName,
                                itemDec = f.ItemDec,
                                itemType = f.ItemType,
                                price = f.Price,
                                categoryId = f.CategoryId,
                                categoryName = c.Name
                            }).ToList();
            return await Task.Run(()=> foodList);
        }

        public async Task<bool> placeOrderforTable(List<placeOrderDTO> placobj)
        {
            var orderid = placobj[0].orderId;
            if (orderid != 0)
            {
                return await insertorderItems(placobj,orderid);
            }
            else
            {
                var ordrdetais = await insertOrder(placobj[0]);
                return await insertorderItems(placobj, ordrdetais.Id);
            }

        }

        public async Task<bool> fishorder(placeOrderDTO orderId)
        {
            var orderitems = _context.OrderItemstbls.Where(x => x.OrderId == orderId.orderId).ToList();

            foreach (var item in orderitems)
            {
                item.Status = "D";
               // _context.OrderItemstbls.Add(item);
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            var ordrdetails = _context.OrderTbls.Where(x => x.Id == orderId.orderId).FirstOrDefault();
            ordrdetails.Orderstatus = "C";
            // _context.OrderTbls.Add(ordrdetails);
            _context.Entry(ordrdetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var tbldetails = _context.TableMasters.Where(x => x.Id == ordrdetails.TableId).FirstOrDefault();
            tbldetails.Status = "F";
          //  _context.TableMasters.Add(tbldetails);
            _context.Entry(tbldetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> Reservetable(placeOrderDTO orderobj)
        {
            

            var tbldetails = _context.TableMasters.Where(x => x.Id == orderobj.tableID).FirstOrDefault();
            tbldetails.Status = "R";
            _context.Entry(tbldetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }
        public async Task<OrderTbl> insertOrder(placeOrderDTO ordobj)
        {
            var ordertbl = new OrderTbl();
            ordertbl.Orderstatus = ordobj.orderstatus;
            ordertbl.EmployeeId = ordobj.employeID;
            ordertbl.TableId = ordobj.tableID;
            ordertbl.OrderTime = DateTime.Now;
            _context.OrderTbls.Add(ordertbl);
            await _context.SaveChangesAsync();
            int orderid = ordertbl.Id;
            var orderdeails = (from o in _context.OrderTbls
                              where o.Id == orderid
                              select o).FirstOrDefault();

            var tbldetails = _context.TableMasters.Where(x => x.Id == ordobj.tableID).FirstOrDefault();
            tbldetails.Status = "B";
            _context.Entry(tbldetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return orderdeails;
        }

        public async Task<bool> insertorderItems(List<placeOrderDTO> placobj,int orderid)
        {
            try
            {
                foreach (var item in placobj)
                {
                    for (int i = 0; i < item.counter; i++)
                    {
                        var itemorder = new OrderItemstbl();
                        itemorder.OrderId = orderid;
                        itemorder.ItemName = item.itemName;
                        itemorder.ItemId = item.itemID;
                        itemorder.Status = item.itemstatus;
                        _context.OrderItemstbls.Add(itemorder);
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}
