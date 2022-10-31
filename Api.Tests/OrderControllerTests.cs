using Api.Controllers;
using Api.Entities;
using Api.Enums;
using Api.Models;
using Api.Persistence.Repositories;
using Api.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests
{
    public class OrderControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ISellerRepository> _sellerRepository;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Order _order;
        Seller seller = new("Joaquim", "11111111111", "joaquim@joaquim.com", "11911111111");
        Product product = new("Boné", 40);
        Product product2 = new("Calça", 70);
        ProductOrder prodOrder1;
        ProductOrder prodOrder2;

        public OrderControllerTests()
        {
            _mapper = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new OrderProfile());
                cfg.AddProfile(new SellerProfile());
                cfg.AddProfile(new ProductProfile());
                cfg.AddProfile(new ProductOrderProfile());
            }).CreateMapper();

            _sellerRepository = new();
            _productRepository = new();
            _orderRepository = new();

            prodOrder1 = new(product, seller, product.Price, 5);
            prodOrder2 = new(product2, seller, product2.Price, 3);
            _order = new(new List<ProductOrder>{prodOrder1, prodOrder2}, seller);
        }

        [Fact]
        public async Task GetById_WithUnexistingId_ReturnsNotFound()
        {
            _orderRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Order) null);
            var controller = new OrderController(
                _orderRepository.Object, _productRepository.Object, _sellerRepository.Object, _mapper
            );

            var result = await controller.GetById(-1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_WithExistingId_ReturnsOrderViewModel()
        {
            _orderRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_order);
            var controller = new OrderController(
                _orderRepository.Object, _productRepository.Object, _sellerRepository.Object, _mapper
            );

            var result = await controller.GetById(1);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultModel = Assert.IsType<OrderViewModel>(okObjectResult.Value);
            Assert.Equal(_order.OrderedProducts.Count, resultModel.OrderedProducts.Count);
        }

        [Fact]
        public async Task Post_WithValidOrder_ReturnsCreatedOrder()
        {
            //Arrange
            _sellerRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Seller) seller);
            _productRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product) product);
            
            var controller = new OrderController(
                _orderRepository.Object, _productRepository.Object, _sellerRepository.Object, _mapper
            );            
            var input = _mapper.Map<Order, OrderInputModel>(_order);

            //Act
            var result = await controller.Post(input);

            //Assert
            var createdObjectResult = Assert.IsType<CreatedAtActionResult>(result);
            var resultModel = Assert.IsType<OrderViewModel>(createdObjectResult.Value);
            Assert.Equal(_order.OrderedProducts.Count, resultModel.OrderedProducts.Count);
        }

        [Fact]
        public async Task Post_WithInvalidSeller_ReturnsBadRequest()
        {
            //Arrange
            _sellerRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Seller) null);
            var controller = new OrderController(
                _orderRepository.Object, _productRepository.Object, _sellerRepository.Object, _mapper
            );            
            var input = _mapper.Map<Order, OrderInputModel>(_order);

            //Act
            var result = await controller.Post(input);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Post_WithInvalidProduct_ReturnsBadRequest()
        {
            //Arrange
            _sellerRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Seller) seller);
            _productRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product) null);
            var controller = new OrderController(
                _orderRepository.Object, _productRepository.Object, _sellerRepository.Object, _mapper
            );            
            var input = _mapper.Map<Order, OrderInputModel>(_order);

            //Act
            var result = await controller.Post(input);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Post_WithoutProductOrders_ReturnsBadRequest()
        {
            //Arrange
            _sellerRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Seller) seller);
            _productRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product) product);
            var controller = new OrderController(
                _orderRepository.Object, _productRepository.Object, _sellerRepository.Object, _mapper
            );            
            var invalidOrder = new Order(new List<ProductOrder>(), seller);
            var input = _mapper.Map<Order, OrderInputModel>(invalidOrder);

            //Act
            var result = await controller.Post(input);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateOrderStatus_WithInvalidId_ReturnsNotFound()
        {
            _orderRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Order) null);
            var controller = new OrderController(
                _orderRepository.Object, _productRepository.Object, _sellerRepository.Object, _mapper
            );

            var result = await controller.UpdateOrderStatus(-1, OrderStatusEnum.Completed);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateOrderStatus_WithInvalidNewStatus_ReturnsBadRequest()
        {
            _orderRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Order) _order);
            var controller = new OrderController(
                _orderRepository.Object, _productRepository.Object, _sellerRepository.Object, _mapper
            );

            var result = await controller.UpdateOrderStatus(1, OrderStatusEnum.Completed);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateOrderStatus_WithValidNewStatus_ReturnsNoContent()
        {
            _orderRepository.Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Order) _order);
            var controller = new OrderController(
                _orderRepository.Object, _productRepository.Object, _sellerRepository.Object, _mapper
            );

            var result = await controller.UpdateOrderStatus(1, OrderStatusEnum.ApprovedPayment);

            Assert.IsType<NoContentResult>(result);
        }
    }
}