using Microsoft.Dynamics.DataEntities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365Entities.EntityClients
{
    public class VendorInventoryClient : BaseClient<WarehouseOnHand>, IClient<WarehouseOnHand>
    {

        public VendorInventoryClient(ILogger logger, Resources entityContext) : base(logger, entityContext)
        {
        }

        /// <summary>
        /// Get warehouseonhand record by the following arguments:
        /// 1. Valid AccountNumber
        /// 2. Valid Warehouse
        /// 3. Valid ItemId
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public Task<WarehouseOnHand> Get(params object[] arguments)
        {
            if(arguments == null || arguments.Length !=3)
            {
                throw new ArgumentException("Invalid filter arguments");
            }

            return null;
        }

        
        /// <summary>
        /// Get an overview by the following arguments:
        /// 1. Valid vendor AccountNumber
        /// 2. Valid Vendor Product Number (Optional)
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WarehouseOnHand>> GetList(params object[] arguments)
        {
            if(arguments == null)
            {
                throw new ArgumentException("Invalid filter arguments");
            }
            var result = new List<WarehouseOnHand>();
            string accountNumber = $"{arguments[0]}";
            string itemId = arguments.Length > 1 ? $"{arguments[1]}" : string.Empty;

            var vendorItemsQuery = (DataServiceQuery<VendorProductDescription>)context.VendorProductDescriptions.Where(key => key.VendorAccountNumber == accountNumber);

            if(!string.IsNullOrEmpty(itemId))
            {
                vendorItemsQuery = (DataServiceQuery<VendorProductDescription>)vendorItemsQuery.Where(key => key.VendorProductNumber == itemId);
            }

            var vendorItems = (await vendorItemsQuery.ExecuteAsync()).ToList();
            if (vendorItems.Any())
            {
                int start = 0;
                int totalItems = 10;
                do
                {
                    var itemsIdList = vendorItems.Skip(start).Take(totalItems).Select(item => item.ItemNumber);

                    var filterParams = itemsIdList.Select(id => string.Format($"(ItemNumber eq '{id}')"));
                    var filter = string.Join(" or ", filterParams);

                    var onhandQuery = (DataServiceQuery<WarehouseOnHand>)context.WarehousesOnHand.AddQueryOption("$filter", filter);

                    var onhandData = (await onhandQuery.ExecuteAsync()).ToList();
                    result.AddRange(onhandData);
                    start += totalItems;
                }
                while (start < vendorItems.Count);

                logger.LogDebug($"WarehouseOnHand overview query result: {result.Count()} rows");

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<WarehouseOnHand> Post(WarehouseOnHand entity)
        {
            throw new NotSupportedException("It is not allowed to post data on the warehouseonhand entity");
        }

        public async Task<WarehouseOnHand> Put(WarehouseOnHand entity)
        {
            throw new NotSupportedException("It is not allowed to Put data on the warehouseonhand entity");
        }

        public async Task<IEnumerable<WarehouseOnHand>> GetAll()
        {
            throw new NotSupportedException("It is not allowed to get all warehouse onhand data, use GetList with valid arguments");
        }

    }
}
