using Microsoft.Dynamics.DataEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace D365Entities.EntityClients
{
    public class BaseClient<T>
    {
        protected ILogger logger;
        protected Resources context;
        public BaseClient(ILogger logger, Resources entityContext)
        {
            context = entityContext ?? throw new ArgumentNullException(nameof(entityContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

    }
}
