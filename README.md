"# A resilient http client example"

This sample demonstrates the use of Polly as resiliency manager for the standard HttpClient


I can not stress enough how nice Polly policies are for ensuring delivery of a message.
https://github.com/App-vNext/Polly

The Queue in this project is just a simple ConcurrentQueue in memory.
In a production environment this would be backed by a database or some other local persistent storage.

The Case Message in these projects is a stripped down example.

Run the SampleEndpoint project.  
	This represents the receiveing endpoint.

Run the Resilient_Client_Sample.
	This project randomly generates cases as queues them for processing.
	This project also posts the message to the receiving endpoint.




