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



        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
