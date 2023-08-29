using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetApiTemplate.UnitTest.TestHelpers
{
    public class ConstructorHelper
    {
        public static IConfiguration GetTestConfigFile()
        {
            var inMemorySettings = new Dictionary<string, string>();
            inMemorySettings.Add("Api:Login", "user/login");
            inMemorySettings.Add("Api:Post", "user/post");
            IConfiguration configuration = new ConfigurationBuilder()
                                            .AddInMemoryCollection(inMemorySettings)
                                            .Build();

            return configuration;
        }
    }
}
