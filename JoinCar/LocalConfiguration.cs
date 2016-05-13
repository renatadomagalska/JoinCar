using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JoinCar
{
    public static class LocalConfiguration
    {
        public static string GoogleClientId => GetSetting("GoogleClientId");

        public static string GoogleClientSecret => GetSetting("GoogleClientSecret");


        public static string FacebookAppId => GetSetting("FacebookAppId");

        public static string FacebookAppSecret => GetSetting("FacebookAppSecret");

        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}