using Api.Entities;
using Api.Models;
using Api.Persistence.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return Ok(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductInputModel model)
        {
            Product product = _mapper.Map<Product>(model);
            try
            {
                await _repository.AddProductAsync(product);
            }
            catch
            {
                return BadRequest();
            }

            ProductViewModel response = _mapper.Map<ProductViewModel>(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
        }
    }
}