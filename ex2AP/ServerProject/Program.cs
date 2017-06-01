using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ServerProject
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// The Class executing the server.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            IController controller = new Controller();
            Model model = new Model();
            IView view = new View(controller);
            controller.BuildController(model, view);
            int port = int.Parse(ConfigurationSettings.AppSettings["port"]);
            string ip_str = ConfigurationSettings.AppSettings["serverAddress"].ToString();
            IPAddress ip_address = IPAddress.Parse(ip_str);
            Server server = new Server(port, view,ip_address);
            server.Start();
        }
    }
}
