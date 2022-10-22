using Api.Controllers;
using Api.Entities;
using Api.Models;
using Api.Persistence.Repositories;
using Api.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests
{
    public class ProductControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly Product _product;

        public ProductControllerTests()
        {
            _mapper = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new ProductProfile());
            }).CreateMapper();

            _mockRepository = new Mock<IProductRepository>();
            _product = new("Computador", 5000);
        }


        [Fact]
        public async Task GetById_WithUnexistingId_ReturnsNotFound()
        {
            //Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product) null);
            var controller = new ProductController(_mockRepository.Object, _mapper);

            //Act
            var result = await controller.GetById(-1);

            //Assert
            Assert.IsType<NotFoundResult>(result);            
        }

        [Fact]
        public async Task GetById_WithExistingId_ReturnsProductViewModel()
        {
            //Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_product);
            var controller = new ProductController(_mockRepository.Object, _mapper);

            //Act
            var result = await controller.GetById(1);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultModel = Assert.IsType<ProductViewModel>(okObjectResult.Value);
            Assert.Equal(_product.Name, resultModel.Name);
            Assert.Equal(_product.Price, resultModel.Price);
        }

        [Fact]
        public async Task Post_WithValidProduct_ReturnsCreatedProduct()
        {
            //Arrange
            var controller = new ProductController(_mockRepository.Object, _mapper);
            var input = _mapper.Map<ProductInputModel>(_product);

            //Act
            var result = await controller.Post(input);

            //Assert
            var createdObjectResult = Assert.IsType<CreatedAtActionResult>(result);
            var resultModel = Assert.IsType<ProductViewModel>(createdObjectResult.Value);
            Assert.Equal(_product.Name, resultModel.Name);
            Assert.Equal(_product.Price, resultModel.Price);
        }

        [Fact]
        public async Task Post_WithInvalidProduct_ReturnsBadRequest()
        {
            //Arrange
            _mockRepository.Setup(repo => repo.AddProductAsync(It.IsAny<Product>()))
                .Throws<OperationCanceledException>();
            var controller = new ProductController(_mockRepository.Object, _mapper);
            var input = _mapper.Map<ProductInputModel>(_product);

            //Act
            var result = await controller.Post(input);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}