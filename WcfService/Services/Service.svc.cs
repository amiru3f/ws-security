using System.ServiceModel;

namespace WcfService
{

    /// <summary>
    /// This class is just a simple calculator to test the if it is working :)
    /// </summary>
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class Service : IService
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public int Sum(int a, int b)
        {
            logger?.Info($"going to calculate summation of {a} and {b}");
            return a + b;
        }
    }
}