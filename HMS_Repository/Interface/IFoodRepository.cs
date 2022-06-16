using HMS_DATAACCESS.Database;
using HMS_Repository.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace HMS_Repository.Interface
{
    public interface IFoodRepository
    {
        Task<IList<cheafupdateitemStatus>> GetCheaforders();
        Task<bool> updateItemStatus(cheafupdateitemStatus itemid);
        Task<bool> InserUpdateFoodItem(FoodItem fooddata);
    }
}
