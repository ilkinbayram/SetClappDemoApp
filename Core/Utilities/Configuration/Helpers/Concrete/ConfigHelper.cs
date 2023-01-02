using Core.Utilities.Configuration.Helpers.Abstract;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Configuration.Helpers.Concrete
{
    public class ConfigHelper : IConfigurationHelper
    {
        private readonly IConfiguration _config;
        public ConfigHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GetDecryptedValue(string? parentKey, string? childKey)
        {
            string encryptedValue = _config.GetSection(parentKey).GetValue<string>(childKey);
            return CustomCryptor.GetCleanData(encryptedValue);
        }

        public string GetDecryptedValue(string? key)
        {
            string encryptedValue = _config.GetValue<string>(key);
            return CustomCryptor.GetCleanData(encryptedValue);
        }

        public string GetValue(string? parentKey, string? childKey)
        {
            return _config.GetSection(parentKey).GetValue<string>(childKey); ;
        }

        public string GetValue(string? key)
        {
            return _config.GetValue<string>(key);
        }
    }
}
