using System.Text;

namespace SetClappDemoApp.MVC.Models
{
    public class ValidationErrorViewModel
    {
        private readonly bool _success;
        public ValidationErrorViewModel(bool success, List<string> errorMessages)
        {
            _success = success;

            StringBuilder responseFormat = new StringBuilder();
            responseFormat.Append($"Sizin toplam {errorMessages.Count} xətanız var.\n\n");
            for (int i = 0; i < errorMessages.Count; i++)
            {
                responseFormat.Append($"{i + 1}. {errorMessages[i]}");
                if (i + 1 < errorMessages.Count)
                    responseFormat.Append("\n");
            }
            ErrorDescription = responseFormat.ToString();
        }

        public bool Success => _success;
        public string ErrorDescription { get; set; }
    }
}
