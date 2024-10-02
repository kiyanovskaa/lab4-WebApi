using AutoMapper;
using kpz_test4.Models;
using kpz_test4.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kpz_test4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Context _context;
    //    private readonly IMapper _mapper;

        public OrderController(Context context)
        {
            _context = context;
     //      _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if(_context.orders == null)
            {
                return NotFound();
            }
            return await _context.orders.ToListAsync();
            /* var orders = await _context.orders.ToListAsync();
             var orderViewModels = _mapper.Map<List<OrderViewModel>>(orders);
             return orderViewModels;*/
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrdersById(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            var order = await _context.orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }        
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            _context.orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrders), new { id = order.id }, order);
        }

        [HttpPut("{id}")]
        public async Task <ActionResult<Order>> PutOrder(int id, Order order)
        {
            
            if (order.id != id)
            {
                return BadRequest();
            }
            _context.Entry(order).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderAvailable(id))
                {
                    return NotFound();
                }
                else throw;
            }

          

            return Ok();
        }

        private bool OrderAvailable(int id)
        {
            return (_context.orders?.Any(x => x.id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _context.orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

/*
  [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if(_context.orders == null)
            {
                return NotFound();
            }
            return await _context.orders.ToListAsync();
            /* var orders = await _context.orders.ToListAsync();
             var orderViewModels = _mapper.Map<List<OrderViewModel>>(orders);
             return orderViewModels;
        }

        [HttpGet("{id}")]
public async Task<ActionResult<OrderViewModel>> GetOrdersById(int id)
{
    var order = await _context.orders.FindAsync(id);
    if (order == null)
    {
        return NotFound();
    }

    var orderViewModel = _mapper.Map<OrderViewModel>(order);
    return orderViewModel;
}

[HttpPost]
public async Task<ActionResult<OrderViewModel>> PostOrder(OrderViewModel orderViewModel)
{
    var order = _mapper.Map<Order>(orderViewModel);

    _context.orders.Add(order);
    await _context.SaveChangesAsync();

    var resultViewModel = _mapper.Map<OrderViewModel>(order);
    return CreatedAtAction(nameof(GetOrders), new { id = resultViewModel.id_view }, resultViewModel);
}

[HttpPut("{id}")]
public async Task<ActionResult> PutOrder(int id, OrderViewModel orderViewModel)
{
    if (id != orderViewModel.id_view)
    {
        return BadRequest();
    }

    var order = _mapper.Map<Order>(orderViewModel);
    _context.Entry(order).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!OrderAvailable(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return Ok();
}

private bool OrderAvailable(int id)
{
    return (_context.orders?.Any(x => x.id == id)).GetValueOrDefault();
}

[HttpDelete("{id}")]
public async Task<ActionResult> DeleteOrder(int id)
{
    var order = await _context.orders.FindAsync(id);
    if (order == null)
    {
        return NotFound();
    }

    _context.orders.Remove(order);
    await _context.SaveChangesAsync();

    return Ok();
}
*/