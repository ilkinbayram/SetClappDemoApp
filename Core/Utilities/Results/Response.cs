namespace Core.Utilities.Results
{
    public class Response : IResponse
    {
        private readonly string _message;
        private readonly string _fullDetail;
        public Response(string message,
                     string fullDetail)
        {
            _message = message;
            _fullDetail = fullDetail;
        }

        public Response(string message)
        {
            _message = message;
            _fullDetail = string.Empty;
        }

        public string Message => _message;
        public string FullDetail => _fullDetail;
    }
}
