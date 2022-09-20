// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Concurrent;
using System.Threading;

namespace resilient_client_sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var message_queue = new ConcurrentQueue<CaseMessage>();
            var cancelation_source = new CancellationTokenSource();
            var cancelation_token = cancelation_source.Token;


            var case_generator = new SampleCaseGenerator(message_queue);
            var post_client = new SamplePostClient(message_queue, new Uri("http://localhost:5192/case_report"));


            case_generator.Start(cancelation_token);
            post_client.Start(cancelation_token);


            Console.ReadLine();
            Console.WriteLine("Stopping Services");
            cancelation_source.Cancel();
            Console.WriteLine("Hit another key to exit.");
            Console.ReadLine();
        }
    }
}