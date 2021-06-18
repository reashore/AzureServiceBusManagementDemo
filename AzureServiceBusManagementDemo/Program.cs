using System;
using System.Threading.Tasks;

namespace AzureServiceBusManagementDemo
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Program
    {
        internal static async Task Main()
        {
            await ManagementConsole.RunReadEvalPrintLoop();
            Console.WriteLine("Done");
        }
    }
}
