using Grpc.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace IntegrationAPI
{
    public class ClientSheduledService : IHostedService
    {
        private System.Timers.Timer timer;
        private Channel channel;
        private NetGrpcService.NetGrpcServiceClient client;

        public ClientSheduledService() { }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            channel = new Channel("localhost:8787", ChannelCredentials.Insecure);
            client = new NetGrpcService.NetGrpcServiceClient(channel);
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(SendMessage);
            timer.Interval = 3300;
            timer.Enabled = true;
            return Task.CompletedTask;
        }

        private async void SendMessage(object source, ElapsedEventArgs e)
        {
            try
            {
                MedicineAvailabilityResponse response = await client.communicateAsync(new MedicineAvailabilityMessage() { MedicineName = "Brufen", MediciQuantity = 10});
                //reading response => do something with it
                Console.WriteLine("PRIMA KLIJENT");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            channel?.ShutdownAsync();
            timer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
