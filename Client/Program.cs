using System.Security.Cryptography.X509Certificates;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculationService.ServiceClient client = new CalculationService.ServiceClient();
            
            X509Store store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = store.Certificates;

            foreach (X509Certificate2 x509 in collection)
            {
                if (x509.Thumbprint == "<YOUR CERTIFICATE THUMBPRINT>")
                {
                    client.ClientCredentials.ClientCertificate.SetCertificate(
                     x509.SubjectName.Name, store.Location, StoreName.TrustedPeople);
                }
            }
            
            var response =  client.Sum(2, 5);
            System.Console.WriteLine(response.ToString());
            System.Console.ReadKey();
        }
    }
}
