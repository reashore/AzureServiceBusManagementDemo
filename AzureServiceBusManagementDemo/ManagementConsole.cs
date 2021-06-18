using System;
using static System.Console;

namespace AzureServiceBusManagementDemo
{
    internal static class ManagementConsole
    {
        private const string ServiceBusConnectionString = "";

        internal static void RunReadEvalPrintLoop()
        {
            ManagementHelper helper = new ManagementHelper(ServiceBusConnectionString);

            bool done = false;
            do
            {
                ForegroundColor = ConsoleColor.Cyan;
                Write(">");
                string commandLine = ReadLine();
                ForegroundColor = ConsoleColor.Magenta;

                if (commandLine == null)
                {
                    continue;
                }
                string[] commands = commandLine.Split(' ');

                try
                {
                    if (commands.Length > 0)
                    {
                        switch (commands[0])
                        {
                            case "createqueue":
                            case "cq":
                                if (commands.Length > 1)
                                {
                                    helper.CreateQueueAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    ForegroundColor = ConsoleColor.Yellow;
                                    WriteLine("Queue path not specified.");
                                }
                                break;

                            case "listqueues":
                            case "lq":
                                helper.ListQueuesAsync().Wait();
                                break;

                            case "getqueue":
                            case "gq":
                                if (commands.Length > 1)
                                {
                                    helper.GetQueueAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    ForegroundColor = ConsoleColor.Yellow;
                                    WriteLine("Queue path not specified.");
                                }
                                break;

                            case "deletequeue":
                            case "dq":
                                if (commands.Length > 1)
                                {
                                    helper.DeleteQueueAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    ForegroundColor = ConsoleColor.Yellow;
                                    WriteLine("Queue path not specified.");
                                }
                                break;

                            case "createtopic":
                            case "ct":
                                if (commands.Length > 1)
                                {
                                    helper.CreateTopicAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    ForegroundColor = ConsoleColor.Yellow;
                                    WriteLine("Topic path not specified.");
                                }
                                break;

                            case "createsubscription":
                            case "cs":
                                if (commands.Length > 2)
                                {
                                    helper.CreateSubscriptionAsync(commands[1], commands[2]).Wait();
                                }
                                else
                                {
                                    ForegroundColor = ConsoleColor.Yellow;
                                    WriteLine("Topic path not specified.");
                                }
                                break;

                            case "listtopics":
                            case "lt":
                                helper.ListTopicsAndSubscriptionsAsync().Wait();
                                break;

                            case "exit":
                                done = true;
                                break;
                        }
                    }
                }
                catch (Exception exception)
                {
                    WriteLine();
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(exception.Message);
                }
            } while (!done);
        }
    }
}
