namespace Core.Utilities.Configuration.Helpers.Abstract
{
    public interface IConfigurationHelper
    {
        string GetDecryptedValue(string? parentKey, string? childKey);
        string GetDecryptedValue(string? key);
        string GetValue(string? parentKey, string? childKey);
        string GetValue(string? key);
    }
}
