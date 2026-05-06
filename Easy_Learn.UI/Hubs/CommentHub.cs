global using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Easy_Learn.UI.Hubs
{
    public class CommentHub : Hub
    {
        private readonly IClient _client;
        public CommentHub(IClient client, IMapper mapper)
        {
            _client = client;
        }

        public async Task CreateComment(string token,string comment , bool Private,int Id)
        {
            var map = new CreateCommentDto()
            {
                Title = comment,
                Private = Private
            };
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue("Bearer",token);
            var apiresponse = await _client.CommentPOSTAsync(Id, map);
            await Clients.Client(Context.ConnectionId).SendAsync("ResponseCreateComment", apiresponse.SuccessFul,apiresponse.Message,apiresponse.Errors);
        }

        public async Task UpdateComment(string token,string comment, int Id)
        {
            var map = new UpdateCommentDto()
            {
                Title = comment
            };
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var apiresponse = await _client.CommentPUTAsync(Id, map);

            await Clients.Client(Context.ConnectionId).SendAsync("ResponseUpdateComment", apiresponse.SuccessFul, apiresponse.Message, apiresponse.Errors);
        }

        public async Task EditComment(string token, int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var comment = (await _client.GetByUserAsync()).FirstOrDefault(o => o.Id == Id);

            await Clients.Client(Context.ConnectionId).SendAsync("ResponseEditComment", comment.Title,comment.Id);
        }

        public async Task DeleteComment(string token, int Id)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            await _client.CommentDELETEAsync(Id);

            await Clients.Client(Context.ConnectionId).SendAsync("ResponseDeleteComment");
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
