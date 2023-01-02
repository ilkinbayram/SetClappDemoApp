using Core.CrossCuttingConcerns.Caching;
using Core.DependencyResolvers;
using Core.Resources.Dictionary;
using Core.Resources.Enum;
using System.Text;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static string Translate(this RequestType key)
        {
            string convertedKey = key.ToString();
            try
            {
                var result = CustomDictionary.GetResource().GetValueOrDefault(convertedKey, convertedKey);
                return result;
            }
            catch (Exception ex)
            {
                return convertedKey;
            }
        }

        public static string Replace(this string content, object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                string propFormat = $"[[{prop.Name}]]";
                var value = prop.GetValue(obj)?.ToString()?.Translate();
                if (string.IsNullOrEmpty(value))
                    content = content.Replace(propFormat, string.Empty);
                content = content.Replace(propFormat, value);
            }
            return content;
        }

        public static string Translate(this RequestStatus key)
        {
            string convertedKey = key.ToString();
            try
            {
                var result = CustomDictionary.GetResource().GetValueOrDefault(convertedKey, convertedKey);
                return result;
            }
            catch (Exception ex)
            {
                return convertedKey;
            }
        }

        public static string Translate(this string key)
        {
            try
            {
                var result = CustomDictionary.GetResource().GetValueOrDefault(key, key);
                return result;
            }
            catch (Exception ex)
            {
                return key;
            }
        }


        //public static string Translate(this string key, short lang_oid)
        //{
        //    try
        //    {
        //        var _cacheManager = CoreInstanceFactory.GetInstance<ICacheManager>();

        //        var serverLocalizationKey = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.ServerCache_ContainerKeyword.ToString(), ChildKeySettings.Server_Language_CachedForAll.ToString());

        //        var allResponse = _cacheManager.Get<Dictionary<short, Dictionary<string, string>>>(serverLocalizationKey);

        //        if (allResponse != null)
        //        {
        //            return allResponse.ContainsKey(lang_oid) && allResponse[lang_oid].ContainsKey(key)
        //                ? allResponse[lang_oid][key]
        //                : key;
        //        }

        //        return key;
        //    }
        //    catch (Exception ex)
        //    {
        //        return key;
        //    }
        //}

        //public static string Translate(this string key, params string[] insteadParameters)
        //{
        //    try
        //    {
        //        var _cacheManager = CoreInstanceFactory.GetInstance<ICacheManager>();

        //        insteadParameters.ToList().ForEach(x =>
        //        {
        //            x = x.Translate();
        //        });

        //        var serverLocalizationKey = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.ServerCache_ContainerKeyword.ToString(), ChildKeySettings.Server_Language_CachedForAll.ToString());

        //        var currentLangOid = Convert.ToInt16(ClientSideStorageHelper.GetLangOidStatic());

        //        var allResponse = _cacheManager.Get<Dictionary<short, Dictionary<string, string>>>(serverLocalizationKey);

        //        if (allResponse != null)
        //        {
        //            return allResponse.ContainsKey(currentLangOid) && allResponse[currentLangOid].ContainsKey(key)
        //                ? string.Format(allResponse[currentLangOid][key], insteadParameters)
        //                : key;
        //        }

        //        return key;
        //    }
        //    catch (Exception ex)
        //    {
        //        return key;
        //    }
        //}

        //public static string Translate(this string key, string insteadParameter)
        //{
        //    try
        //    {
        //        var _cacheManager = CoreInstanceFactory.GetInstance<ICacheManager>();

        //        // TODO : Hard Code Should Be Refactored

        //        var serverLocalizationKey = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.ServerCache_ContainerKeyword.ToString(), ChildKeySettings.Server_Language_CachedForAll.ToString());

        //        var currentLangOid = Convert.ToInt16(ClientSideStorageHelper.GetLangOidStatic());

        //        var allResponse = _cacheManager.Get<Dictionary<short, Dictionary<string, string>>>(serverLocalizationKey);

        //        if (allResponse != null)
        //        {
        //            return allResponse.ContainsKey(currentLangOid) && allResponse[currentLangOid].ContainsKey(key)
        //                ? string.Format(allResponse[currentLangOid][key], insteadParameter.Translate())
        //                : key;
        //        }

        //        return key;
        //    }
        //    catch (Exception ex)
        //    {
        //        return key;
        //    }
        //}

        //public static string GetStaticMediaURL(this string configKey)
        //{
        //    var resultRead = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.MediaServiceURL_ContainerKeyword.ToString(), configKey);

        //    return resultRead;
        //}
    }
}
