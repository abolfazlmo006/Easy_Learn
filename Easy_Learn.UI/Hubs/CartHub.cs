using System.Net.Http.Headers;

namespace Easy_Learn.UI.Hubs
{
    public class CartHub : Hub
    {
        private readonly IClient _client;

        public CartHub(IClient client)
        {
            _client = client;
        }

        public async Task AddToCart(string token, int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var apiresponse = await _client.OrderDetailPOSTAsync(Id);

            await Clients.Client(Context.ConnectionId).SendAsync("ResponseAddToCart", apiresponse.SuccessFul, apiresponse.Message);
        }
    }
}
