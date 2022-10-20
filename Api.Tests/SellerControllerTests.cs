using Api.Controllers;
using Api.Entities;
using Api.Models;
using Api.Persistence.Repositories;
using Api.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests;

public class SellerControllerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ISellerRepository> _mockRepository;
    private readonly Seller _seller;

    public SellerControllerTests()
    {
        _mapper = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile(new SellerProfile());
        }).CreateMapper();

        _seller = new Seller("José", "11111111111", "jose@jose.com", "11999999999");
        _mockRepository = new Mock<ISellerRepository>();
    }

    [Fact]
    public async Task GetById_WithUnexistingId_ReturnsNotFound()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Seller)null);

        var controller = new SellerController(_mockRepository.Object, _mapper);

        //Act
        var result = await controller.GetById(-1);

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetById_WithExistingSeller_ReturnsAViewModel()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(_seller);

        var controller = new SellerController(_mockRepository.Object, _mapper);
        SellerViewModel expectedSellerViewModel = _mapper.Map<SellerViewModel>(_seller);

        //Act
        var result = await controller.GetById(0);
        var okObjectResult = result as OkObjectResult;
        var resultModel = okObjectResult.Value as SellerViewModel;

        //Assert
        Assert.IsType<SellerViewModel>(okObjectResult.Value);
        Assert.Equal(_seller.Name, resultModel.Name);
        Assert.Equal(_seller.Cpf, resultModel.Cpf);
    }

    [Fact]
    public async Task GetAll_ReturnsSellerList()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestSellers());
        var controller = new SellerController(_mockRepository.Object, _mapper);

        //Act
        var result = await controller.GetAll();
        var okObjectResult = result as OkObjectResult;

        //Assert
        var sellerViewResult = Assert.IsType<List<SellerViewModel>>(okObjectResult.Value);
        Assert.Equal(2, sellerViewResult.Count());
    }

    public List<Seller> GetTestSellers()
    {
        var sellers = new List<Seller>();
        sellers.Add(new Seller("Maria", "22222222222", "maria@maria.com", "11911111111"));
        sellers.Add(new Seller("João", "33333333333", "joao@joao.com", "11900000000"));
        return sellers;
    }
}