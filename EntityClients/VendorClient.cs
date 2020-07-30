using Microsoft.Dynamics.DataEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace D365Entities.EntityClients
{
    public class VendorClient : BaseClient<VendorV2>, IClient<VendorV2>
    {
        public VendorClient(ILogger logger, Resources entityContext) : base(logger, entityContext)
        {

        }

        public async Task<VendorV2> Get(params object[] arguments)
        {
            arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));

            var vendor = $"{arguments[0]}";

            var query = context.VendorsV2.AddQueryOption(nameof(VendorV2.VendorAccountNumber), vendor);

            var result = await query.ExecuteAsync();
            return result.FirstOrDefault();
        }

        public Task<IEnumerable<VendorV2>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VendorV2>> GetList(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public Task<VendorV2> Post(VendorV2 entity)
        {
            throw new NotImplementedException();
        }

        public Task<VendorV2> Put(VendorV2 entity)
        {
            throw new NotImplementedException();
        }
    }
}
