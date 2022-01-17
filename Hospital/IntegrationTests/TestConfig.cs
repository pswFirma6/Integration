using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests
{
    public class TestConfig
    {

        public string Environment { get; set; }

        public TestConfig()
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.test.json")
                            .Build();

            var apiKey = config["ApiKey"];
            string s = apiKey.ToString();

            Environment = config["Environment"].ToString();

        }
    }
}
