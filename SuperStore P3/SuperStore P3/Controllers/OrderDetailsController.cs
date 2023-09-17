using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using EcoPower_Logistics.Repositories;

namespace Controllers
{
    [Authorize]
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsController(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var result = await _orderDetailsRepository.GetOrderDetailsAsync();
            return Ok(result);
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var result = await _orderDetailsRepository.GetOrderDetailByIdAsync(id);
            return Ok(result);
        }


        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailsId,OrderId,ProductId,Quantity,Discount")] OrderDetail orderDetail)
        {
            var result = await _orderDetailsRepository.AddOrderDetailAsync(orderDetail);
            return Ok(result);
        }


        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailsId,OrderId,ProductId,Quantity,Discount")] OrderDetail orderDetail)
        {
            var result = await _orderDetailsRepository.UpdateOrderDetailAsync(orderDetail);
            return Ok(result);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = _orderDetailsRepository.DeleteOrderDetailAsync(id);
            return Ok(result);
        }
    }
}
