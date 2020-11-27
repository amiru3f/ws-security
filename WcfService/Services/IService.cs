using System.ServiceModel;

namespace WcfService
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign, Namespace = "https://docs.microsoft.com/en-us/previous-versions/dotnet/articles/ms951273(v=msdn.10)?redirectedfrom=MSDN")]
    public interface IService
    {
        [OperationContract]
        int Sum(int a, int b);
    }
}