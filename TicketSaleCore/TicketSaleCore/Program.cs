using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace TicketSaleCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel(
                (o) =>
               {
                   o.UseHttps(new X509Certificate2(@"Taler-WST.crt"));
                 //  o.UseHttps(new X509Certificate2(@"Tn.cer"));
               })
                .UseUrls("https://*:443")
                .UseContentRoot(Directory.GetCurrentDirectory())
                // .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }//
    }
}
