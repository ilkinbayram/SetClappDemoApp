using System.Collections.Generic;

namespace Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }
        bool IsProcessBroken { get; }
        List<Response> Responses { get; }
    }
}
