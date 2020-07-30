using D365Entities.Services.DTO;
using Microsoft.Dynamics.DataEntities;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.OData.Client;
using System;
using System.Linq;

namespace D365Entities.Services
{
    public class ResourceService
    {
        static AuthenticationResult authenticationResult;

        public static Resources Create(ServiceClientOptions serviceClientOptions, ILogger logger)
        {
            var authenticationContext = new AuthenticationContext(serviceClientOptions.AuthenticationContextURL, false);
            var resources = new Resources(new Uri(serviceClientOptions.Url))
            {
                EntityParameterSendOption = EntityParameterSendOption.SendOnlySetProperties,

                SaveChangesDefaultOptions = SaveChangesOptions.BatchWithSingleChangeset
            };

            resources.BuildingRequest += (sender, eventArgs) =>
            {
                bool tokenIsValid = authenticationResult != null && DateTimeOffset.Now.UtcTicks > authenticationResult.ExpiresOn.UtcTicks;

                if (!tokenIsValid)
                {
                    logger.LogDebug($"Get token from {serviceClientOptions.AuthenticationContextURL}");

                    var authenticationResultTask = authenticationContext.AcquireTokenAsync(serviceClientOptions.Resource, new ClientCredential(serviceClientOptions.ClientId, serviceClientOptions.ClientSecret));
                    authenticationResultTask.Wait();
                    authenticationResult = authenticationResultTask.Result;
                    
                    logger.LogDebug($"Token received");
                }

                eventArgs.Headers.Add("Authorization", authenticationResult.CreateAuthorizationHeader());
            };

            resources.Configurations.RequestPipeline.OnEntryStarting((writingEntryArguments) =>
            {
                writingEntryArguments.Entry.Properties = writingEntryArguments.Entry.Properties.Where(property => property.Value != null);
            });

            return resources;
        }
    }
}
