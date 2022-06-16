using HMS_DATAACCESS.Database;
using HMS_Repository.Interface;
using HMS_Repository.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly IFoodRepository _FoodRepository;
        public FoodItemsController(IFoodRepository foodRepository)
        {
            this._FoodRepository = foodRepository;
        }
        [HttpGet]
        [Route("GetCheaforders")]
        public async Task<IList<cheafupdateitemStatus>> GetCheaforders()
        {
            return await _FoodRepository.GetCheaforders();
        }
        [HttpPost]
        [Route("updateItemStatus")]
        public async Task<bool> updateItemStatus(cheafupdateitemStatus itemid)
        {
            return await _FoodRepository.updateItemStatus(itemid);
        }

        [HttpPost]
        [Route("InserUpdateFoodItem")]
        public async Task<bool> InserUpdateFoodItem(FoodItem fooddata)
        {
            return await _FoodRepository.InserUpdateFoodItem(fooddata);
        }
    }
}
