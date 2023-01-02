using Core.Resources.OperationResources.Model;
using Core.Resources.OperationResources.Results;
using Operation.Concrete.Base;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Service.Concrete.Operations
{
    public class HttpPostFunction<TResult> : Function<TResult>
        where TResult : class, IOperationResponse, new()
    {
        public override async Task<TResult> DoExecuteAsync(string uri, DataMessage content)
        {
            var myContent = JsonSerializer.Serialize(content);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using HttpClient client = new();

            var result = await client.PostAsync(uri, byteContent);

            TResult response = new TResult();
            response.IsSuccess = result.IsSuccessStatusCode;
            response.Message = string.Empty;
            response.Result = result.Content;

            return response;
        }
    }
}
