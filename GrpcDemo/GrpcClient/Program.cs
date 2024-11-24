// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

//var input = new HelloRequest { Name = "Minh" };
//var channel = GrpcChannel.ForAddress("https://localhost:7067"); // call server
//var client = new Greeter.GreeterClient(channel);

//var reply = await client.SayHelloAsync(input);

//Console.WriteLine(reply.Message);

var channel = GrpcChannel.ForAddress("https://localhost:7067");
var customerClient = new Customer.CustomerClient(channel);

var clientRequested = new CustomerLookupModel { UserId = 2 };

var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

Console.WriteLine($"{customer.FirstName} {customer.LastName}");

Console.WriteLine();
Console.WriteLine("New Customer List");
Console.WriteLine();

using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
    while (await call.ResponseStream.MoveNext())
    {
        var currentCustomer = call.ResponseStream.Current;

        Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}: {currentCustomer.EmailAddress} {currentCustomer.Age} {currentCustomer.IsActived}");
    }
}

Console.ReadLine();
