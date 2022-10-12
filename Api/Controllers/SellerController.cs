using Api.Entities;
using Api.Models;
using Api.Persistence.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerRepository _repository;
        private readonly IMapper _mapper;
        public SellerController(ISellerRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sellers = await _repository.GetAllAsync();
            var sellersViewModel = _mapper.Map<List<SellerViewModel>>(sellers);
            return Ok(sellersViewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var seller = await _repository.GetByIdAsync(id);
            if (seller == null)
            {
                return NotFound();
            }

            var sellerViewModel = _mapper.Map<SellerViewModel>(seller);
            return Ok(sellerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SellerInputModel model)
        {
            Seller seller = _mapper.Map<Seller>(model);
            await _repository.AddSellerAsync(seller);

            return CreatedAtAction(nameof(GetById), new { id = seller.Id }, seller);
        }
    }
}