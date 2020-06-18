using Ecommerce;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManagementClient
{
    class Program
    {
         static async Task Main(string[] args)
        {
            var channel = new Channel("192.168.0.5", 5001, ChannelCredentials.Insecure);
            var client = new OrderManagementService.OrderManagementServiceClient(channel);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            var value = new StringValue { Value = "OrderId" };
            using var streamCall = client.updateOrders() ;

            try
            {
                
                for (int i = 0; i < 10; i++)
                {
                   await streamCall.RequestStream.WriteAsync( new Order { Id = i.ToString() } );
                }

               
                await streamCall.RequestStream.CompleteAsync();

                var response = await streamCall;
                Console.WriteLine($"Status: {response.Value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.ReadKey();

        }
    }
}
