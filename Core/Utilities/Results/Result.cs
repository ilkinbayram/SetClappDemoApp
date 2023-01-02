using System.Collections.Generic;
using System.Linq;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        private readonly List<Response> _responses;
        private bool _hasResponseMessages => _responses != null && _responses.Count > 0;
        public Result(bool success, bool isProcessBroken, string message) : this(success, isProcessBroken)
        {
            _responses.Add(new Response(message));
        }

        public Result(bool success, bool isProcessBroken, string message, string fullDetail) : this(success, isProcessBroken)
        {
            _responses.Add(new Response(message, fullDetail));
        }

        public Result(bool success, bool isProcessBroken, List<string> messages) : this(success, isProcessBroken)
        {
            if (messages != null && messages.Count > 0)
            {
                foreach (var msgOne in messages)
                {
                    _responses.Add(new Response(msgOne));
                }
            }
        }
        public Result(bool success, bool isProcessBroken, List<Response> responses) : this(success, isProcessBroken)
        {
            if (responses != null)
            {
                foreach (var resOne in responses)
                {
                    _responses.Add(resOne);
                }
            }
        }

        public Result(bool success, bool isProcessBroken)
        {
            Success = success;
            IsProcessBroken = isProcessBroken;
            if (Responses == null) _responses = new List<Response>();
        }

        public bool HasResponseMessages => _hasResponseMessages;

        public void SetMessages(string message, string fullDetail)
        {
            if (!_responses.Any(x => x.Message == message))
                _responses.Add(new Response(message, fullDetail));
        }
        public void SetMessages(string message)
        {
            if (!_responses.Any(x => x.Message == message))
                _responses.Add(new Response(message));
        }

        public void SetMessages(List<string> messageList)
        {
            if (messageList != null && messageList.Count > 0)
            {
                foreach (var messageOne in messageList)
                {
                    if (!_responses.Any(x => x.Message == messageOne))
                        _responses.Add(new Response(messageOne));
                }
            }
        }

        public bool Success { get; }
        public bool IsProcessBroken { get; }
        public List<Response> Responses => _responses;
    }
}
