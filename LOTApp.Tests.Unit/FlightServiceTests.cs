using AutoMapper;
using LOTApp.Business.Services;
using LOTApp.Core.DTOs;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;
using LOTApp.DataAccess.Repositories;
using Moq;

namespace LOTApp.Tests.Unit
{

    public class FlightServiceTests
    {
        [Fact]
        public void Get_Always_ReturnsAllFlights()
        {
            // Arrange
            var inputData = new FlightViewModel[]
            {
                new(){Id=2   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="KXR",ArrivalLocation= "BEW",PlaneType= (PlaneType) 0},
             new(){Id=3   ,FlightNumber="GH321",DepartTime=DateTime.UtcNow, DepartLocation="FZA",ArrivalLocation= "ASD",PlaneType=(PlaneType) 1},
             new(){Id=4   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
             new(){Id=5   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
             new() { Id=6   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="UGA",ArrivalLocation= "PEH",PlaneType= (PlaneType)0 },
            }.AsEnumerable();

            var expectedData = new FlightViewModel[]
           {
                new(){Id=2   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="KXR",ArrivalLocation= "BEW",PlaneType= (PlaneType) 0},
              new(){Id=3   ,FlightNumber="GH321",DepartTime=DateTime.UtcNow, DepartLocation="FZA",ArrivalLocation= "ASD",PlaneType=(PlaneType) 1},
              new(){Id=4   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
              new(){Id=5   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
              new() { Id=6   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="UGA",ArrivalLocation= "PEH",PlaneType= (PlaneType)0 },
           }.AsEnumerable();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightViewModel>().ReverseMap();
            }).CreateMapper();

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(r => r.AllEntities)
                .Returns(mapper.Map<IEnumerable<Flight>>(expectedData).AsQueryable());

            var mockService = new FlightService(mapper, mockRepository.Object);


            // Act
            var result = mockService.Get();

            // Assert
            Assert.NotNull(result);

            Assert.Equal(expectedData, result, new FlightViewModelComparer());
        }

        [Fact]
        public void Get_Always_ReturnsFilteredFlights()
        {
            // Arrange
            var inputData = new Flight[]
            {
                new(){Id=2   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="KXR",ArrivalLocation= "BEW",PlaneType= (PlaneType) 0},
             new(){Id=3   ,FlightNumber="GH321",DepartTime=DateTime.UtcNow, DepartLocation="FZA",ArrivalLocation= "ASD",PlaneType=(PlaneType) 1},
             new(){Id=4   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
             new(){Id=5   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
             new() { Id=6   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="UGA",ArrivalLocation= "PEH",PlaneType= (PlaneType)0 },
            }.AsQueryable();

            var expectedData = new FlightViewModel[]
           {
              new(){Id=4   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
              new(){Id=5   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
           }.AsQueryable();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightViewModel>().ReverseMap();
            }).CreateMapper();

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(r => r.AllEntities)
                .Returns(mapper.Map<IEnumerable<Flight>>(expectedData).AsQueryable());

            var mockService = new FlightService(mapper, mockRepository.Object);


            // Act
            var result = mockService.Get(departLocation: "FTK");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedData, result, new FlightViewModelComparer());
        }

        [Fact]
        public void Get_Always_ReturnsEmptyCollection()
        {
            // Arrange
            var inputData = new Flight[]
            {
                new(){Id=2   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="KXR",ArrivalLocation= "BEW",PlaneType= (PlaneType) 0},
             new(){Id=3   ,FlightNumber="GH321",DepartTime=DateTime.UtcNow, DepartLocation="FZA",ArrivalLocation= "ASD",PlaneType=(PlaneType) 1},
             new(){Id=4   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
             new(){Id=5   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
             new() { Id=6   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="UGA",ArrivalLocation= "PEH",PlaneType= (PlaneType)0 },
            }.AsQueryable();

            var expectedData = new FlightViewModel[]
           {

           }.AsQueryable();

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(r => r.AllEntities).Returns(inputData);


            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightViewModel>().ReverseMap();
            }).CreateMapper();

            var mockService = new FlightService(mapper, mockRepository.Object);


            // Act
            var result = mockService.Get(flightNumber: "XX999");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.Equal(expectedData, result, new FlightViewModelComparer());
        }

        [Fact]
        public void Get_Always_ReturnsSingleElementCollection()
        {
            // Arrange
            var inputData = new Flight[]
            {
                new(){Id=2   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="KXR",ArrivalLocation= "BEW",PlaneType= (PlaneType) 0},
             new(){Id=3   ,FlightNumber="GH321",DepartTime=DateTime.UtcNow, DepartLocation="FZA",ArrivalLocation= "ASD",PlaneType=(PlaneType) 1},
             new(){Id=4   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
             new(){Id=5   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
             new() { Id=6   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="UGA",ArrivalLocation= "PEH",PlaneType= (PlaneType)0 },
            }.AsQueryable();

            var expectedData = new FlightViewModel[]
           {
             new(){Id=5   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
           }.AsQueryable();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightViewModel>().ReverseMap();
            }).CreateMapper();

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(r => r.AllEntities)
                .Returns(mapper.Map<IEnumerable<Flight>>(expectedData).AsQueryable());

            var mockService = new FlightService(mapper, mockRepository.Object);


            // Act
            var result = mockService.Get(id: 5);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedData, result, new FlightViewModelComparer());
        }

        [Fact]
        public void GetSingle_IfExists_ReturnsFlight()
        {
            // Arrange
            var inputData = new Flight[]
             {
              new(){Id=2  ,FlightNumber="AS123",DepartTime=new DateTime(2024, 4, 15, 10, 0, 0), DepartLocation="KXR",ArrivalLocation= "BEW",PlaneType= (PlaneType) 0},
              new(){Id=3  ,FlightNumber="GH321",DepartTime=new DateTime(2024, 4, 18, 12, 30, 0), DepartLocation="FZA",ArrivalLocation= "ASD",PlaneType=(PlaneType) 1},
              new(){Id=4  ,FlightNumber="AS123",DepartTime=new DateTime(2024, 4, 22, 17, 15, 0), DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
              new(){Id=5  ,FlightNumber="AS123",DepartTime=new DateTime(2024, 4, 25, 8, 45, 0), DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
              new() { Id=6  ,FlightNumber="AS123",DepartTime=new DateTime(2024, 4, 29, 14, 0, 0), DepartLocation="UGA",ArrivalLocation= "PEH",PlaneType= (PlaneType)0 },
             }.AsQueryable();

            var expectedData = new FlightViewModel
            {
                Id = 3,
                FlightNumber = "GH321",
                DepartTime = new DateTime(2024, 4, 18, 12, 30, 0),
                DepartLocation = "FZA",
                ArrivalLocation = "ASD",
                PlaneType = (PlaneType)1
            };

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightViewModel>().ReverseMap();
            }).CreateMapper();

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(r => r.AllEntities)
                .Returns(inputData);

            var mockService = new FlightService(mapper, mockRepository.Object);


            // Act
            var searchData = 3;
            var result = mockService.GetSingle(searchData);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData, result, new FlightViewModelComparer());
        }

        [Fact]
        public void GetSingle_IfMissing_ReturnsNull()
        {
            // Arrange
            var inputData = new Flight[]
             {
              new(){Id=2  ,FlightNumber="AS123",DepartTime=new DateTime(2024, 4, 15, 10, 0, 0), DepartLocation="KXR",ArrivalLocation= "BEW",PlaneType= (PlaneType) 0},
              new(){Id=3  ,FlightNumber="GH321",DepartTime=new DateTime(2024, 4, 18, 12, 30, 0), DepartLocation="FZA",ArrivalLocation= "ASD",PlaneType=(PlaneType) 1},
              new(){Id=4  ,FlightNumber="AS123",DepartTime=new DateTime(2024, 4, 22, 17, 15, 0), DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
              new(){Id=5  ,FlightNumber="AS123",DepartTime=new DateTime(2024, 4, 25, 8, 45, 0), DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
              new() { Id=6  ,FlightNumber="AS123",DepartTime=new DateTime(2024, 4, 29, 14, 0, 0), DepartLocation="UGA",ArrivalLocation= "PEH",PlaneType= (PlaneType)0 },
             }.AsQueryable();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightViewModel>().ReverseMap();
            }).CreateMapper();

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(r => r.AllEntities)
                .Returns(inputData);

            var mockService = new FlightService(mapper, mockRepository.Object);

            // Act
            var searchData = 7;
            var result = mockService.GetSingle(searchData);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_WithValidData_ReturnsFlight()
        {
            // Arrange
            var newDataDTO = new CreateFlightDTO
            {
                FlightNumber = "GH321",
                DepartTime = DateTime.UtcNow,
                DepartLocation = "FZA",
                ArrivalLocation = "ASD",
                PlaneType = PlaneType.Airbus.ToString(),
            };

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, CreateFlightDTO>().ReverseMap();
                cfg.CreateMap<Flight, FlightViewModel>().ReverseMap();

            }).CreateMapper();

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(r => r.Create(It.IsAny<Flight>()))
                .Returns(Task.FromResult(mapper.Map<Flight>(newDataDTO)))
                .Verifiable();

            var service = new FlightService(mapper, mockRepository.Object);

            // Act
            var result = await service.Create(newDataDTO);

            // Assert
            Assert.NotNull(result);
            mockRepository.VerifyAll();
        }

        [Fact]
        public async void Update_WithValidData_ReturnsFlight()
        {
            // Arrange
            var baseData = new FlightViewModel
            {
                Id = 3,
                FlightNumber = "GH321",
                DepartTime = new DateTime(2024, 4, 18, 12, 30, 0),
                DepartLocation = "FZA",
                ArrivalLocation = "ASD",
                PlaneType = (PlaneType)1
            };

            var expected = new FlightViewModel
            {
                Id = 3,
                FlightNumber = "GH321",
                DepartTime = new DateTime(2024, 4, 18, 12, 30, 0),
                DepartLocation = "KTW",
                ArrivalLocation = "KRK",
                PlaneType = (PlaneType)1
            };

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightViewModel>().ReverseMap();
            }).CreateMapper();

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(r => r.Update(It.IsAny<Flight>()))
                .Returns(Task.FromResult(mapper.Map<Flight>(expected)))
                .Verifiable();

            var service = new FlightService(mapper, mockRepository.Object);

            // Act
            var result = await service.Update(expected);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected, result, new FlightViewModelComparer());
            mockRepository.VerifyAll();
        }
    }
}
