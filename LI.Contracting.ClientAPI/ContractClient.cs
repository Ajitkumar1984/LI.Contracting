using LI.Contracting.EntityDTO;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LI.Contracting.ClientAPI
{
    public  class ContractClient:IContractClient  
    {
        private string _baseUrl;
        private HttpClient httpClient;
        private ContractOption _ContractOption;
        public ContractClient(string baseUrl, ContractOption ContractOption)
        {
            _baseUrl = baseUrl;
            _ContractOption = ContractOption;
            InitializeClient();
        }

        private HttpClient InitializeClient()
        {
             httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = new TimeSpan(0, 30, 0);
            return httpClient;
        }       
        private void AddHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                if (!string.IsNullOrEmpty(header.Value))
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        #region MGAAPI
        public async Task<MGADTO> GetMGAById(string mgaid)
        {
            var response = await httpClient.GetAsync(string.Format(_ContractOption.MGAEndpoint.MGAById,mgaid));
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MGADTO>(apiResult);
            return result;
        }
        public async Task<List<MGADTO>> ListAllMGA()
        {
            var response = await httpClient.GetAsync(_ContractOption.MGAEndpoint.ListMGA);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<MGADTO>>(apiResult);
            return result;
        }
        public async Task<int> CreateMGA(MGADTO MGAModel)
        {
            var response = await httpClient.PostAsJsonAsync(_ContractOption.MGAEndpoint.CreateMGA, MGAModel);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        public async Task<int> UpdateMGA(string mgaid, MGADTO MGAModel)
        {
            var response = await httpClient.PutAsJsonAsync(string.Format(_ContractOption.MGAEndpoint.UpdateMGA, mgaid), MGAModel);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        public async Task<int> DeleteMGA(string mgaid)
        {
            var response = await httpClient.DeleteAsync(string.Format(_ContractOption.MGAEndpoint.DeleteMGA, mgaid));
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        #endregion

        #region Carrier
        public async Task<CarrierDTO> GetCarrierById(string id)
        {
            var response = await httpClient.GetAsync(string.Format(_ContractOption.CarrierEndpoint.CarrierById, id));
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CarrierDTO>(apiResult);
            return result;
        }
        public async Task<List<CarrierDTO>> ListAllCarrier()
        {
            var response = await httpClient.GetAsync(_ContractOption.CarrierEndpoint.ListCarrier);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<CarrierDTO>>(apiResult);
            return result;
        }
        public async Task<int> CreateCarrier(CarrierDTO model)
        {
            var response = await httpClient.PostAsJsonAsync(_ContractOption.CarrierEndpoint.CreateCarrier, model);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        public async Task<int> UpdateCarrier(string id, CarrierDTO model)
        {
            var response = await httpClient.PutAsJsonAsync(string.Format(_ContractOption.CarrierEndpoint.UpdateCarrier, id), model);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        public async Task<int> DeleteCarrier(string id)
        {
            var response = await httpClient.DeleteAsync(string.Format(_ContractOption.CarrierEndpoint.DeleteCarrier, id));
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        #endregion

        #region Advisor
        public async Task<AdvisorDTO> GetAdvisorById(string id)
        {
            var response = await httpClient.GetAsync(string.Format(_ContractOption.AdvisorEndpoint.AdvisorById, id));
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AdvisorDTO>(apiResult);
            return result;
        }
        public async Task<List<AdvisorDTO>> ListAllAdvisor()
        {
            var response = await httpClient.GetAsync(_ContractOption.AdvisorEndpoint.ListAdvisor);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<AdvisorDTO>>(apiResult);
            return result;
        }
        public async Task<int> CreateAdvisor(AdvisorDTO Model)
        {
            var response = await httpClient.PostAsJsonAsync(_ContractOption.AdvisorEndpoint.CreateAdvisor, Model);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        public async Task<int> UpdateAdvisor(string id, AdvisorDTO Model)
        {
            var response = await httpClient.PutAsJsonAsync(string.Format(_ContractOption.AdvisorEndpoint.UpdateAdvisor, id), Model);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        public async Task<int> DeleteAdvisor(string id)
        {
            var response = await httpClient.DeleteAsync(string.Format(_ContractOption.AdvisorEndpoint.DeleteAdvisor, id));
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        #endregion

        #region Contract
        public async Task<ContractDTO> GetContractById(string id)
        {
            var response = await httpClient.GetAsync(string.Format(_ContractOption.ContractEndpoint.ContractById, id));
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ContractDTO>(apiResult);
            return result;
        }
        public async Task<List<ContractDTO>> ListAllContract()
        {
            var response = await httpClient.GetAsync(_ContractOption.ContractEndpoint.ListContract);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ContractDTO>>(apiResult);
            return result;
        }
        public async Task<int> CreateContract(ContractDTO Model)
        {
            var response = await httpClient.PostAsJsonAsync(_ContractOption.ContractEndpoint.CreateContract, Model);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        public async Task<int> UpdateContract(string id, ContractDTO Model)
        {
            var response = await httpClient.PutAsJsonAsync(string.Format(_ContractOption.ContractEndpoint.UpdateContract, id), Model);
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        public async Task<int> DeleteContract(string id)
        {
            var response = await httpClient.DeleteAsync(string.Format(_ContractOption.ContractEndpoint.DeleteContract, id));
            var apiResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(apiResult);
            return result;
        }
        #endregion
    }
}
