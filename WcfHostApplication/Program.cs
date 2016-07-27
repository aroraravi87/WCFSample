using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WcfServiceContainer;

namespace WcfHostApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8731/WcfServiceContainer/ShoppingCartService/");

            using (WebServiceHost host = new WebServiceHost(typeof(ShoppingCartService), baseAddress))
            {
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);
                ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IShoppingCartService), new WebHttpBinding(), "");
                ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                stp.HttpHelpPageEnabled = false;
                host.Open();
                Console.WriteLine("Service is up and running on {0} ", baseAddress);
                Console.WriteLine("Press enter to quit ");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
