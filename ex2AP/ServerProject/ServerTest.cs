using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    class ServerTest
    {
        static void Main(string[] args)
        {
            IController controller = new Controller();
            Model model = new Model();
            IView view = new View(controller);
            controller.buildController(model, view);
            int port = int.Parse(ConfigurationSettings.AppSettings["port"]);
            Server server = new Server(port, view);
            server.start();
        }
    }
}
