using Grpc.Core;

namespace GrpcServer.Services;

public class CustomersService : Customer.CustomerBase
{
    private readonly ILogger<CustomersService> _logger;

    public CustomersService(ILogger<CustomersService> logger)
    {
        _logger = logger;
    }

    public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
    {
        CustomerModel output = new CustomerModel();

        if (request.UserId == 1)
        {
            output.FirstName = "Vien";
            output.LastName = "Minh";
        }
        else if (request.UserId == 2)
        {
            output.FirstName = "Thanh";
            output.LastName = "Hai";
        }
        else
        {
            output.FirstName = "Quoc";
            output.LastName = "Son";
        }

        return Task.FromResult(output);
    }

    public override async Task GetNewCustomers(
        NewCustomerRequest request, 
        IServerStreamWriter<CustomerModel> responseStream, 
        ServerCallContext context)
    {
        List<CustomerModel> customers = new List<CustomerModel>
        {
            new CustomerModel
            {
                FirstName = "Hoang",
                LastName = "Anh",
                EmailAddress = "hoanganhgialai@gmail.com",
                Age = 21,
                IsActived = true
            },
            new CustomerModel
            {
                FirstName = "Hoang",
                LastName = "Huy",
                EmailAddress = "hoanghuy@gmail.com",
                Age = 21,
                IsActived = true
            },
            new CustomerModel
            {
                FirstName = "Khanh",
                LastName = "Linh",
                EmailAddress = "khanhlinh@gmail.com",
                Age = 21,
                IsActived = true
            },
        };

        foreach (var cust in customers)
        {
            await responseStream.WriteAsync(cust);
        }
    }
}
