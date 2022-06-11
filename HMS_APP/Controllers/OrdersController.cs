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
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _orderRepository;
        public OrdersController(IOrdersRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("GetpendingOrders")]
        public async Task<IList<OrderDTO>> GetpendingOrders()
        {
            return await _orderRepository.GetpendingOrders();
        }
        [HttpGet]
        [Route("GetCompletedCount")]
        public async Task<dashboardCards> GetCompletedCount()
        {
            return await  _orderRepository.getorderscount();
        }
        [HttpGet]
        [Route("getTableData")]
        public async Task<List<tabledata>> getTableData()
        {
            return await _orderRepository.getTableData();
        }
        [HttpGet]
        [Route("getBookingTableorderDetails")]
        public async Task<List<bookingorderDeatils>> getBookingTableorderDetails()
        {
            return await _orderRepository.getBookingTableorderDetails();
        }

        [HttpGet]
        [Route("getfoodItemsList")]
        public async Task<IList<FoodItemList>> getfoodItemsList()
        {
            return await _orderRepository.getfooditemsList();
        }

        [HttpPost]
        [Route("placeOrderforTable")]
        public async Task<bool> placeOrderforTable(List<placeOrderDTO> placobj)
        {
            return await _orderRepository.placeOrderforTable(placobj);
        }

        [HttpPost]
        [Route("fishorder")]
        public async Task<bool> fishorder(placeOrderDTO orderId)
        {
            return await _orderRepository.fishorder(orderId);
        }
        [HttpPost]
        [Route("Reservetable")]
        public async Task<bool> Reservetable(placeOrderDTO orderId)
        {
            return await _orderRepository.Reservetable(orderId);
        }
    }
}
