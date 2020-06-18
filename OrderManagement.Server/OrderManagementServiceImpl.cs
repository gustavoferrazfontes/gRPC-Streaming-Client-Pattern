using Ecommerce;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace OrderManagement.Server
{
    public class OrderManagementServiceImpl:OrderManagementService.OrderManagementServiceBase
    {
        public async override Task<StringValue> updateOrders(IAsyncStreamReader<Order> requestStream, ServerCallContext context)
        {
              while (await requestStream.MoveNext())
            {

                Console.WriteLine(requestStream.Current.Id);
            }

                return new StringValue { Value = "done" };            
        }
    }
}
