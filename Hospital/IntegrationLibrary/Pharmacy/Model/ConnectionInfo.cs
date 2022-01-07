using IntegrationLibrary.Shared.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class ConnectionInfo : ValueObject
    {
        public string ApiKey { get; set; }
        public string FileProtocol { get; set; }
        public string Url { get; set; }

        public ConnectionInfo(string apiKey, string fileProtocol, string url)
        {
            ApiKey = apiKey;
            FileProtocol = fileProtocol;
            Url = url;
            ValidateProtocol();
            ValidateUrl();
            ValidateApiKey();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ApiKey;
            yield return FileProtocol;
            yield return Url;
        }

        private void ValidateProtocol()
        {
            if (!FileProtocol.Equals("HTTP") || !FileProtocol.Equals("SFTP")) { }
              //  throw new ConnectionInfoException("File protocol is not supported");
        }

        private void ValidateUrl()
        {
            if (string.IsNullOrEmpty(Url))
                throw new ConnectionInfoException("Url is empty");
        }

        private void ValidateApiKey()
        {
            if (string.IsNullOrEmpty(ApiKey))
                throw new ConnectionInfoException("Api key is not valid");
        }

    }

    public class ConnectionInfoException : Exception
    {
        public ConnectionInfoException(String message) : base(message) { }

    }

}
