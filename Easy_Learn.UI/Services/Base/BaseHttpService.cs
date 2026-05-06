using Easy_Learn.UI.Contracts;
using System.Net.Http.Headers;

namespace Easy_Learn.UI.Services.Base
{
    public class BaseHttpService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IClient _client;

        public BaseHttpService(IClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid> { Errors = ex.Response, Success = false, Massage = "error" };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid> { Success = false, Massage = "Not Found" };
            }
            else
            {
                return new Response<Guid> { Success = false, Massage = "Error" };
            }
        }
        protected void AddBearerToken()
        {
            if (_localStorageService.Exists("token"))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _localStorageService.GetStorageValue<string>("token"));
            }
        }
    }
}
