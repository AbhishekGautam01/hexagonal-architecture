
namespace Accounting.Application.Exceptions
{
    internal sealed class AccountNotFoundException: ApplicationException
    {
        internal AccountNotFoundException(string message): base(message) { }
    }
}
