using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class ConnectionInfo
    {
        public string ApiKey { get; set; }
        public string FileProtocol { get; set; }
        public string Url { get; set; }

        public ConnectionInfo(string apiKey, string fileProtocol, string url)
        {
            ApiKey = apiKey;
            FileProtocol = fileProtocol;
            Url = url;
        }
    }
}
