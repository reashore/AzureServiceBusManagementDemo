using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus.Management;

using static System.Console;

namespace AzureServiceBusManagementDemo
{
    internal class ManagementHelper
    {
        private readonly ManagementClient _managementClient;

        public ManagementHelper(string connectionString)
        {
            _managementClient = new ManagementClient(connectionString);
        }

        public async Task CreateQueueAsync(string queuePath)
        {
            Write("Creating queue {0}...", queuePath);
            var description = GetQueueDescription(queuePath);
            var createdDescription = await _managementClient.CreateQueueAsync(description);
            WriteLine("Done!");
        }

        public async Task DeleteQueueAsync(string queuePath)
        {
            Write("Deleting queue {0}...", queuePath);
            await _managementClient.DeleteQueueAsync(queuePath);
            WriteLine("Done!");
        }

        public async Task ListQueuesAsync()
        {
            IEnumerable<QueueDescription> queueDescriptions = await _managementClient.GetQueuesAsync();
            WriteLine("Listing queues...");

            foreach (QueueDescription queueDescription in queueDescriptions)
            {
                WriteLine("\t{0}", queueDescription.Path);
            }

            WriteLine("Done!");
        }

        public async Task GetQueueAsync(string queuePath)
        {
            QueueDescription queueDescription = await _managementClient.GetQueueAsync(queuePath);


            WriteLine($"Queue description for { queuePath }");
            WriteLine($"    Path:                                   { queueDescription.Path }");
            WriteLine($"    MaxSizeInMB:                            { queueDescription.MaxSizeInMB }");
            WriteLine($"    RequiresSession:                        { queueDescription.RequiresSession }");
            WriteLine($"    RequiresDuplicateDetection:             { queueDescription.RequiresDuplicateDetection }");
            WriteLine($"    DuplicateDetectionHistoryTimeWindow:    { queueDescription.DuplicateDetectionHistoryTimeWindow }");
            WriteLine($"    LockDuration:                           { queueDescription.LockDuration }");
            WriteLine($"    DefaultMessageTimeToLive:               { queueDescription.DefaultMessageTimeToLive }");
            WriteLine($"    EnableDeadLetteringOnMessageExpiration: { queueDescription.EnableDeadLetteringOnMessageExpiration }");
            WriteLine($"    EnableBatchedOperations:                { queueDescription.EnableBatchedOperations }");
            WriteLine($"    MaxSizeInMegabytes:                     { queueDescription.MaxSizeInMB }");
            WriteLine($"    MaxDeliveryCount:                       { queueDescription.MaxDeliveryCount }");
            WriteLine($"    Status:                                 { queueDescription.Status }");
        }

        public async Task CreateTopicAsync(string topicPath)
        {
            Write("Creating topic {0}...", topicPath);
            TopicDescription description = await _managementClient.CreateTopicAsync(topicPath);
            WriteLine("Done!");
        }

        public async Task CreateSubscriptionAsync(string topicPath, string subscriptionName)
        {
            Write("Creating subscription {0}/subscriptions/{1}...", topicPath, subscriptionName);
            var description = await _managementClient.CreateSubscriptionAsync(topicPath, subscriptionName);
            WriteLine("Done!");
        }

        public async Task ListTopicsAndSubscriptionsAsync()
        {
            IEnumerable<TopicDescription> topicDescriptions = await _managementClient.GetTopicsAsync();
            WriteLine("Listing topics and subscriptions...");

            foreach (TopicDescription topicDescription in topicDescriptions)
            {
                WriteLine("\t{0}", topicDescription.Path);
                IEnumerable<SubscriptionDescription> subscriptionDescriptions = await _managementClient.GetSubscriptionsAsync(topicDescription.Path);

                foreach (SubscriptionDescription subscriptionDescription in subscriptionDescriptions)
                {
                    WriteLine("\t\t{0}", subscriptionDescription.SubscriptionName);
                }
            }

            WriteLine("Done!");
        }

        private static QueueDescription GetQueueDescription(string path)
        {
            return new (path)
            {
                RequiresDuplicateDetection = true,
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(10),
                RequiresSession = true,
                MaxDeliveryCount = 20,
                DefaultMessageTimeToLive = TimeSpan.FromHours(1),
                EnableDeadLetteringOnMessageExpiration = true
            };
        }
    }
}
