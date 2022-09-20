using System;
using System.Collections.Concurrent;
using System.Threading;

namespace resilient_client_sample
{
    public class SampleCaseGenerator
    {
        private readonly ConcurrentQueue<CaseMessage> _messageQueue;

        public SampleCaseGenerator(ConcurrentQueue<CaseMessage> message_queue)
        {
            _messageQueue = message_queue;
        }

        public void Start(CancellationToken cancellationToken)
        {
            var t = new Thread(Start)
            {
                IsBackground = true
            };
            t.Start(cancellationToken);
        }

        private void Start(object? obj)
        {
            var token = (CancellationToken)obj;
            var random = new Random(DateTime.Now.Second);

            while (!token.IsCancellationRequested)
            {
                var wait = random.Next(2000, 6000);
                Thread.Sleep(wait);
                var caseMessage = CaseMessage.MakeTestCase();
                ConsoleHelper.WriteLineInColor("Case " + caseMessage.SerialNumber + " created", ConsoleColor.Green);
                _messageQueue.Enqueue(caseMessage);
            }
        }
    }
}