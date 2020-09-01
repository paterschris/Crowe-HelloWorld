using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_World.Configuration
{
    public class GlobalConfigurationSettings
    {

        public class Rootobject
        {
            public Logging Logging { get; set; }
            public string AllowedHosts { get; set; }
            public DatabaseConfig DatabaseConfig { get; set; }
        }

        public class Logging
        {
            public Loglevel LogLevel { get; set; }
        }

        public class Loglevel
        {
            public string Default { get; set; }
            public string Microsoft { get; set; }
            public string MicrosoftHostingLifetime { get; set; }
        }

        public class DatabaseConfig
        {
            public string Location { get; set; }
            public string Name { get; set; }
        }

    }
}
