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
    public class CustomerRepository : GeneralRepository<Customer, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;

        public CustomerRepository(Address address, string request = "Customers/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public HttpStatusCode Post(AddCustomerVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "AddCustomer", content).Result;
            return result.StatusCode;
        }
        public async Task<List<AddCustomerVM>> GetUser(string id)
        {
            List<AddCustomerVM> entities = new List<AddCustomerVM>();

            using (var response = await httpClient.GetAsync(request + "GetIdProfile/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<AddCustomerVM>>(apiResponse);
            }
            return entities;
        }

    }
}
