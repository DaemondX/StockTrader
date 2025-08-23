
namespace StockTrader.Domain.Exceptions
{
    public class DifferentPCNameException : Exception
    {
        public string Username { get; }
        public string MachineName { get; }
        public string PCName { get; }

        public DifferentPCNameException(string userName, string machineName, string pcName)
        {
            Username = userName;
            MachineName = machineName;
            PCName = pcName;
        }
    }
}