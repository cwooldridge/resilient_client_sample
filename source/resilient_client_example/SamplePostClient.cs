using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using Polly;
using Polly.Retry;

namespace resilient_client_sample
{

    public class SamplePostClient
    {
        private readonly HttpClient _client;
        private readonly Uri _endpoint;

        private readonly ConcurrentQueue<CaseMessage> _messageQueue;
        private readonly AsyncRetryPolicy _policy;

        private int _eventualFailures;
        private int _eventualSuccesses;
        private int _retries;
        private int _totalRequests;


        public SamplePostClient(ConcurrentQueue<CaseMessage> message_queue, Uri endpoint)
        {
            _messageQueue = message_queue;
            _endpoint = endpoint;

            _client = new HttpClient();

            // Define our policy:

            _policy = Policy.Handle<Exception>().WaitAndRetryForeverAsync(
                attempt => TimeSpan.FromMilliseconds(1000), // Wait 1000ms between each try.
                (exception, calculatedWaitDuration) => // Capture some info for logging!
                {
                    // This is your new exception handler! 
                    // Tell the user what they've won!
                    ConsoleHelper.WriteLineInColor("Log, then retry: " + exception.Message, ConsoleColor.Yellow);
                    _retries++;
                });


        }


        public void Start(CancellationToken cancellationToken)
        {
            var t = new Thread(Run)
            {
                IsBackground = true
            };
            t.Start(cancellationToken);
        }

        private async void Run(object? obj)
        {
            var cancellationToken = (CancellationToken)obj;

            while (!cancellationToken.IsCancellationRequested)
            {
                if (_messageQueue.IsEmpty)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Total requests made                 : " + _totalRequests);
                    Console.WriteLine("Requests which eventually succeeded : " + _eventualSuccesses);
                    Console.WriteLine("Retries made to help achieve success: " + _retries);
                    Console.WriteLine("Requests which eventually failed    : " + _eventualFailures);

                    Thread.Sleep(1000);
                    continue;
                }

                Console.WriteLine("Queue Length " + _messageQueue.Count);

                CaseMessage? message;
                if (_messageQueue.TryPeek(out message))
                    try
                    {
                        Console.WriteLine("Attempting Post of message " + message.CorrelationId);

                        _totalRequests++;
                        await _policy.ExecuteAsync(async token =>
                        {
                            // This code is executed within the Policy 
                            // Make a request and get a response
                            var response = await _client.PostAsync(_endpoint, JsonContent.Create(message), token);

                            // Display the response message on the console
                            ConsoleHelper.WriteLineInColor("Response : " + response.StatusCode, ConsoleColor.Green);
                            _eventualSuccesses++;
                        }, cancellationToken);

                        if (_messageQueue.TryDequeue(out message))
                            Console.WriteLine("Case " + message.SerialNumber + " successfully sent and dequeued.");
                    }
                    catch (Exception e)
                    {
                        ConsoleHelper.WriteLineInColor(
                            "Request " + message.CorrelationId + " eventually failed with: " + e.Message,
                            ConsoleColor.Red);
                        _eventualFailures++;
                    }
            }
        }
    }
}