using AutoMapper;
using LOTApp.Business.Services;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;
using LOTApp.DataAccess.Repositories;
using Moq;

namespace LOTApp.IntegrationTests.ControllerTests
{
    public class FlightControllerTests : IDisposable
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        public FlightControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void Get_Always_ReturnsAllFlights()
        {
            // Arrange
            var mockFlights = new Flight[]
            {
                new(){Id=2   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="KXR",ArrivalLocation= "BEW",PlaneType= (PlaneType) 0},
               // new(){Id=3   ,FlightNumber="GH321",DepartTime=DateTime.UtcNow, DepartLocation="FZA",ArrivalLocation= "ASD",PlaneType=(PlaneType) 1},
               // new(){Id=4   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
               // new(){Id=5   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
               // new() { Id=6   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="UGA",ArrivalLocation= "PEH",PlaneType= (PlaneType)0 },
            }.AsQueryable();

            var mockFlightVMs = new FlightViewModel[]
           {
                new(){Id=2   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="KXR",ArrivalLocation= "BEW",PlaneType= (PlaneType) 0},
               // new(){Id=3   ,FlightNumber="GH321",DepartTime=DateTime.UtcNow, DepartLocation="FZA",ArrivalLocation= "ASD",PlaneType=(PlaneType) 1},
               // new(){Id=4   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 2},
               // new(){Id=5   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="FTK",ArrivalLocation= "GMY",PlaneType=(PlaneType) 0},
               // new() { Id=6   ,FlightNumber="AS123",DepartTime=DateTime.UtcNow, DepartLocation="UGA",ArrivalLocation= "PEH",PlaneType= (PlaneType)0 },
           }.AsQueryable();

            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(r => r.AllEntities).Returns(mockFlights);


            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<IQueryable<FlightViewModel>>(
                It.IsAny<IQueryable<Flight>>())).Returns(mockFlightVMs);

            var mockManager = new FlightService(mockMapper.Object, mockRepository.Object);

            // Act
            var result = await mockManager.Get();

            // Assert
            Assert.NotNull(result);

            Assert.Collection((result),
                r =>
                {
                    Assert.Equal("AS123", r.FlightNumber);
                }
            );

        }

        public void GetById_IfExists_ReturnsFlight()
        {

        }

        public void GetById_IfMissing_Returns404()
        {

        }

        public void Post_WithValidData_SavesFlight()
        {

        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
