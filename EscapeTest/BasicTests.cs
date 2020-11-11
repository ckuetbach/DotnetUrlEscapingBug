using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using EscapeWeb;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace EscapeTest
{
    public class BasicTests 
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper _testOutputHelper;

        public BasicTests(WebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("/api/values", "a/b")]
        [InlineData("/api/values", "a%2Fb")]
        public async Task Get_With_Keys_In_Query(string url, string id)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var requestUri = url+ "?id=" + HttpUtility.UrlEncode(id);
            _testOutputHelper.WriteLine($"requested uri: {requestUri}");
            var response = await client.GetAsync(requestUri).ConfigureAwait(false);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var reflectedId = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(id, reflectedId);
        }
        
        [Theory]
        [InlineData("/api/values", "a/b")]
        [InlineData("/api/values", "a%2Fb")]
        public async Task Get_With_Keys_In_Path_TestServer(string url, string id)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var requestUri = url+ "/(" + HttpUtility.UrlEncode(id) + ")/";
            _testOutputHelper.WriteLine($"requested uri: {requestUri}");
            var response = await client.GetAsync(requestUri).ConfigureAwait(false);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var reflectedId = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(id, reflectedId);
        }
        
        [Theory]
        [InlineData("http://localhost:5000/api/values", "a/b")]
        [InlineData("http://localhost:5000/api/values", "a%2Fb")]
        public async Task Get_With_Keys_In_Path_Server(string url, string id)
        {
            // Arrange
            var client = new HttpClient();

            // Act
            var requestUri = url+ "/(" + HttpUtility.UrlEncode(id) + ")/";
            _testOutputHelper.WriteLine($"requested uri: {requestUri}");
            var response = await client.GetAsync(requestUri).ConfigureAwait(false);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var reflectedId = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(id, reflectedId);
        }
    }
}