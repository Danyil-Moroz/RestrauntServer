namespace RestrauntServer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RestrauntServer.Helpers;
    using RestrauntServer.Models;
    using RestrauntServer.Services;

    [Route("[controller]")]
    public class ClientOrdersController : Controller
    {
        private readonly ClientORderService _service;

        public ClientOrdersController(ClientORderService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Orders")]
        public async Task<IActionResult> Index(int id)
        {
            var result = JsonHelper.ConvertToJsonString(await _service.GetAllOrdersForUser(id));

            if (string.IsNullOrWhiteSpace(result.Content))
            {
                Response.StatusCode = StatusCodes.Status204NoContent;
            }
            return result ;
        }

       
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Details(int clientId , int id)
        {
            var result = JsonHelper.ConvertToJsonString(await _service.GetOrderDetails(clientId,id));
            if (string.IsNullOrWhiteSpace(result.Content))
            {
                Response.StatusCode = StatusCodes.Status204NoContent;
            }
            return result;
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            var result = new ContentResult();
            try
            {
                if (order != null)
                {
                    await _service.CreateOrder(order);
                    result.StatusCode = Response.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    result.StatusCode = Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            catch (Exception)
            {
                result.StatusCode = Response.StatusCode = StatusCodes.Status400BadRequest;
            }
           
            return result;

        }

        //// GET: ClientOrders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(order);
        //}

        //// POST: ClientOrders/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ClientId,Status,Address,TableNumber,IsDelivery,DeliveryDate,CreatedDate,Id")] Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(order);
        //}

        //// GET: ClientOrders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: ClientOrders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var order = await _context.Order.FindAsync(id);
        //    _context.Order.Remove(order);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderExists(int id)
        //{
        //    return _context.Order.Any(e => e.Id == id);
        //}
    }
}
