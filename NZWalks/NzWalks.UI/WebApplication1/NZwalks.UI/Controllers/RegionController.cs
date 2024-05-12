using Microsoft.AspNetCore.Mvc;
using NZwalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZwalks.UI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();

            try
            {
                var client = httpClientFactory.CreateClient();

                var responseMessage = await client.GetAsync("https://localhost:7041/api/region");

                responseMessage.EnsureSuccessStatusCode();

                response.AddRange(await responseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception)
            {    throw;  }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionModel request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7041/api/region"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Region");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var responseMessage = await client.GetFromJsonAsync<EditRegionModel>($"https://localhost:7041/api/region/{id}");

                if (responseMessage is not null)
                {
                    return View(responseMessage);
                }

                return View(null);
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRegionModel request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7041/api/region/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<EditRegionModel>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Region");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditRegionModel request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7041/api/region/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Region");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
