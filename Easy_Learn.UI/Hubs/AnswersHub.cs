using System.Net.Http.Headers;

namespace Easy_Learn.UI.Hubs
{
    public class AnswersHub : Hub
    {
        private readonly IClient _client;

        public AnswersHub(IClient client)
        {
            _client = client;
        }

        public async Task CreateAnswer(string token, string answer, int questionId)
        {
            var map = new CreateAnswerQuestionDto
            {
                Description = answer,
                QuestionCourseId = questionId
            };
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var apiresponse = await _client.AnswerQuestionPOSTAsync(map);
            
            await Clients.Client(Context.ConnectionId).SendAsync("ResponseCreateAnswer", apiresponse.SuccessFul, apiresponse.Message, apiresponse.Errors);

            if (apiresponse.SuccessFul)
            {
                var a = await _client.QuestionCourseGETAsync(questionId);
                var answer_last = a.AnswerQuestions.LastOrDefault();

                await Clients.All.SendAsync("ResponseAllCreateAnswer", answer_last.Description, answer_last.Full_Name, answer_last.Id, answer_last.CreatedTime);

            }
        }

        public async Task DeleteAnswer(string token, int answerId)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var apiresponse = await _client.AnswerQuestionDELETEAsync(answerId);
            }
            finally
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ResponseDeleteAnswer");

                await Clients.All.SendAsync("ResponseAllDeleteAnswer", answerId);

            }

        }
    }
}
