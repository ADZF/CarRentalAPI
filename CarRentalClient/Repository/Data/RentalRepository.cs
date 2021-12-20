using CarRentalAPI.Models;
using CarRentalAPI.ViewModel;
using CarRentalClient.Base.Urls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalClient.Repository.Data
{
    public class RentalRepository : GeneralRepository<Rental, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;

        public RentalRepository(Address address, string request = "Rentals/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public HttpStatusCode Post(RentalVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "AddRental", content).Result;
            return result.StatusCode;
        }
        public async Task<List<RentalVM>> GetRental(string id)
        {
            List<RentalVM> entities = new List<RentalVM>();

            using (var response = await httpClient.GetAsync(request + "GetIdRental/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RentalVM>>(apiResponse);
            }
            return entities;
        }
    }
}