namespace Accounting.Infrastructure.Exceptions
{
    public class CustomerNotFoundException: InfrastructureException
    {
        internal CustomerNotFoundException(string message)
            : base(message)
        { }
    }
}
