using System;
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
        [InlineData("/api/values/?id=a%2Fb", "a/b")]     // Encoded a/b
        [InlineData("/api/values/?id=a%252Fb", "a%2Fb")] // Double encoded a/b
        public async Task Get_With_Keys_In_Query(string requestUri, string expectedId)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            _testOutputHelper.WriteLine($"requested uri: {requestUri}");
            var response = await client.GetAsync(requestUri).ConfigureAwait(false);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var reflectedId = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(expectedId, reflectedId);
        }
        
        [Theory]
        [InlineData("/api/values/(a%2Fb)/", "a/b")]     // Encoded a/b
        [InlineData("/api/values/(a%252Fb)", "a%2Fb")]  // Double encoded a/b
        public async Task Get_With_Keys_In_Path_TestServer(string requestUri, string expectedId)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            _testOutputHelper.WriteLine($"requested uri: {requestUri}");
            var response = await client.GetAsync(requestUri).ConfigureAwait(false);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var reflectedId = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(expectedId, reflectedId);
        }
        
        [Theory]
        [InlineData("/api/values/(a%2Fb)/", "a/b")]     // Encoded a/b
        [InlineData("/api/values/(a%252Fb)", "a%2Fb")]  // Double encoded a/b
        public async Task Get_With_Keys_In_Path_Server(string requestUri, string expectedId)
        {
            // Arrange
            var client = new HttpClient{BaseAddress = new Uri("http://localhost:5000/")};
            

            // Act
            _testOutputHelper.WriteLine($"requested uri: {requestUri}");
            var response = await client.GetAsync(requestUri).ConfigureAwait(false);
            
            // Assert
            response.EnsureSuccessStatusCode();
            var reflectedId = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(expectedId, reflectedId);
        }
    }
}