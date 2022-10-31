using Api.Entities;
using Api.Enums;
using Api.Models;
using Api.Persistence.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ISellerRepository _sellerRepository;

        public OrderController(IOrderRepository repository, IProductRepository productRepository, ISellerRepository sellerRepository, IMapper mapper)
        {
            _sellerRepository = sellerRepository;
            _productRepository = productRepository;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Order order = await _repository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            return Ok(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderInputModel modelInput)
        {
            if (!modelInput.OrderedProducts.Any()) return BadRequest();

            Seller seller = await _sellerRepository.GetByIdAsync(modelInput.SellerId);
            if (seller == null) return BadRequest("Invalid seller");

            List<ProductOrder> productOrders = new List<ProductOrder>();

            foreach (var productOrderInput in modelInput.OrderedProducts)
            {
                Product product = await _productRepository.GetByIdAsync(productOrderInput.ProductId);
                if (product == null) return BadRequest("Invalid product");

                double unitPrice = product.Price;

                ProductOrder productOrder = new(product, seller, unitPrice, productOrderInput.Quantity);
                productOrders.Add(productOrder);
            }

            Order order = new(productOrders, seller);
            try
            {
                await _repository.AddOrderAsync(order);
            }
            catch
            {
                return BadRequest();
            }

            OrderViewModel response = _mapper.Map<OrderViewModel>(order);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody]OrderStatusEnum newStatus)
        {
            Order order = await _repository.GetByIdAsync(orderId);
            if (order == null) return NotFound();

            if (!order.UpdateStatus(newStatus))
            {
                return BadRequest();
            }

            await _repository.UpdateStatusAsync(order);
            return NoContent();
        }

    }
}