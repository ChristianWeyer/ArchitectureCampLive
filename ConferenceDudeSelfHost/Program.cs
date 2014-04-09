using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using ServerHosting;

namespace ConferenceDudeSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Assembly.Load("ConferenceDudeServices");
            
            using (WebApp.Start<Startup>("http://localhost:7777"))
            {
                Console.WriteLine("Server läuft...");
                Console.ReadLine();
            }
                
        }
    }
}
