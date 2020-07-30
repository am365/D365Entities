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
    public class PurchaseOrderClient : BaseClient<PurchaseOrderHeaderV2>, IClient<PurchaseOrderHeaderV2>
    {
        public PurchaseOrderClient(ILogger logger, Resources entityContext) : base(logger, entityContext)
        {

        }

        public async Task<PurchaseOrderHeaderV2> Get(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PurchaseOrderHeaderV2>> GetAll()
        {
            throw new NotSupportedException("It is not allowed to get all purchase orders");
        }

        public Task<IEnumerable<PurchaseOrderHeaderV2>> GetList(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public async Task<PurchaseOrderHeaderV2> Post(PurchaseOrderHeaderV2 entity)
        {
            var vendorClient = new VendorClient(logger, context);

            var vendor = (await vendorClient.Get(entity.OrderVendorAccountNumber)) ?? throw new Exception($"Vendor does not exists");
            
            context.AddToPurchaseOrderHeadersV2(entity);

            foreach (var line in entity.PurchaseOrderLinesV2)
            {
                context.AddToPurchaseOrderLinesV2(line);
            }

            //await context.SaveChangesAsync();

            return entity;            
        }

        public Task<PurchaseOrderHeaderV2> Put(PurchaseOrderHeaderV2 entity)
        {
            throw new NotImplementedException();
        }
    }
}
