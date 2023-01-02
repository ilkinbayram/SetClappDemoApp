using System.Text;

namespace Core.Utilities.Generators
{
    public class StringGenerator
    {
        public static string GenerateDocumentNumber(DateTime documentDate, int existCount)
        {
            var yearEnd = documentDate.Year.ToString().Substring(2, 2);
            var month = documentDate.Month.ToString();
            var count = existCount+1;
            StringBuilder counterBuilder = new StringBuilder();
            for (int i = count.ToString().Length; i < 4; i++)
                counterBuilder.Append("0");
            counterBuilder.Append(count.ToString());
            if (month.Length == 1) month = $"0{month}";
            return $"Q-{yearEnd}-{month}-{counterBuilder}";
        }

        public static string GenerateSecurityToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        }
    }
}
