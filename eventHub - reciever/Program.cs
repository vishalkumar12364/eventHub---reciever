using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace eventHub___reciever
{
    class Program
    {
        private const string EventHubConnectionString = "Endpoint=sb://vishaleventhub2.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=vJGFV5DaKLcCsZRSdxIkj767MdqYfncTH9PFr3M2V+g=";
        private const string EventHubName = "vishaleventhub";
        private const string StorageContainerName = "vishalcontainer";  
        private const string StorageAccountName = "vishalstorageaccount";
        private const string StorageAccountKey = "/pDUDiSdNcn8xqY/VqiDqPw0YjrvKgVc9vda5dHeWihs67aEbdI3oQV9tuRUMzXN1oXlxjhwcJSnLf6pGkWriQ==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol = https; AccountName=vishalstorageaccount;AccountKey=/pDUDiSdNcn8xqY/VqiDqPw0YjrvKgVc9vda5dHeWihs67aEbdI3oQV9tuRUMzXN1oXlxjhwcJSnLf6pGkWriQ==;EndpointSuffix=core.windows.net", StorageAccountName, StorageAccountKey);

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProccessor...");

            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();

            await eventProcessorHost.UnregisterEventProcessorAsync();


        }
    }
}
