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
   public class FoodRepository : IFoodRepository
    {
        private readonly HMSDBDbContext _context;

        public FoodRepository(HMSDBDbContext context)
        {
            _context = context;
        }

        public async Task<IList<cheafupdateitemStatus>> GetCheaforders()
        {
            var preparinngitems = (from s in  _context.OrderItemstbls
                                   join ot in _context.OrderTbls on s.OrderId equals ot.Id
                                   join t in _context.TableMasters on ot.TableId equals t.Id
                                   where s.Status =="P"
                                   orderby s.Id ascending
                                  select new cheafupdateitemStatus
                                  { 
                                      itemID = s.Id,
                                      itemName = s.ItemName,
                                      itemstatus=s.Status,
                                      employeID=ot.EmployeeId,
                                      orderId=s.OrderId,
                                      tableID=ot.TableId,
                                      tablename=t.Name
                                  }).ToList();
            return await Task.Run(() => preparinngitems);
        }
        public async Task<bool> updateItemStatus(cheafupdateitemStatus itemid)
        {
            var orderitems = _context.OrderItemstbls.Where(x => x.Id == itemid.itemID).FirstOrDefault();
                orderitems.Status = "D";
                _context.Entry(orderitems).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            return true;
        }

    }
}
