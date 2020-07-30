using System;
using System.Collections.Generic;
using System.Text;

namespace D365Entities.Services.DTO
{
    public class ServiceClientOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Url { get; set; }

        public string AuthenticationContextURL { get; set; }

        public string Resource { get; set; }
    }
}
