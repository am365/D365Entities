using Microsoft.Dynamics.DataEntities;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365Entities.EntityClients
{
    public class LegalEntityClient : BaseClient<LegalEntity>, IClient<LegalEntity>
    {
        public LegalEntityClient(ILogger logger, Resources entityContext) : base(logger, entityContext)
        {

        }

        public async Task<LegalEntity> Get(params object[] arguments)
        {
            arguments = arguments ?? throw new ArgumentException(nameof(arguments));

            var query = context.LegalEntities.AddQueryOption("$filter", $"{nameof(LegalEntity.LegalEntityId)} eq '{arguments[0]}'");

            return (await query.ExecuteAsync()).FirstOrDefault();

        }

        public async Task<IEnumerable<LegalEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LegalEntity>> GetList(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public async Task<LegalEntity> Post(LegalEntity entity)
        {
            throw new NotSupportedException("It is not allowed to create a legalentity by ODATA Entity");
        }

        public Task<LegalEntity> Put(LegalEntity entity)
        {
            throw new NotSupportedException("It is not allowed to create a legalentity by ODATA Entity");
        }
    }
}
