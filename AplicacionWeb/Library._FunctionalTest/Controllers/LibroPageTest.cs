using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using Library.FunctionalTest;
using Library.FunctionalTest.Helpers;
using Library.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Library._FunctionalTest.Controllers
{
    public class LibroPageTest :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public LibroPageTest(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        #region Pruebas
        [Fact]
        public async Task GetIndex()
        {
            // Arrange
            var defaultPage = await _client.GetAsync("/Libros");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act
            //var response = await _client.SendAsync(
            //    (IHtmlFormElement)content.QuerySelector("form[id='messages']"),
            //    (IHtmlButtonElement)content.QuerySelector("button[id='deleteAllBtn']"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);

        }
        [Fact]
        public async Task GetAdd()
        {
            // Arrange
            var defaultPage = await _client.GetAsync("/Libros/Create");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            var form = (IHtmlFormElement)content.QuerySelector("form[id='savefrm']");
            var button = (IHtmlButtonElement)content.QuerySelector("button[id='btnSave']");
            //Act
            var response = await _client.SendAsync(
                form,
                button,
                new Dictionary<string, string>
                {
                    ["ISBN"] = "ISBN"+DateTime.Now.Second,
                    ["Titulo"] = "ISBN" + DateTime.Now.Second,
                    ["Sinopsis"] = "ISBN" + DateTime.Now.Second,
                    ["NPaginas"] = "200",
                    ["Active"] = "true",
                    ["EditorialesId"] = "2",
                    ["AutoresIds"] = "1",

                });

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
           

        }
        #endregion
    }
}
