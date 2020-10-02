using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using Library.FunctionalTest.Helpers;
using Library.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Library.FunctionalTest.Controllers
{
   public  class IndexPageTest :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public IndexPageTest(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        #region snippet2
        [Fact]
        public async Task GetIndex()
        {
            // Arrange
            var defaultPage = await _client.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act
            //var response = await _client.SendAsync(
            //    (IHtmlFormElement)content.QuerySelector("form[id='messages']"),
            //    (IHtmlButtonElement)content.QuerySelector("button[id='deleteAllBtn']"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
           
        }
        #endregion
    }
}
