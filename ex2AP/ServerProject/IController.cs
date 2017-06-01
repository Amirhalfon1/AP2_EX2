using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace ServerProject
{
    public interface IController
    {
        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="m">The m.</param>
        void setModel(IModel m);
        
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="v">The v.</param>
        void setView(IView v);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="problem">The problem.</param>
        /// <param name="client">The client.</param>
        /// <returns>string</returns>
        string executeCommand(String problem, TcpClient client);

        /// <summary>
        /// Builds the controller.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="view">The view.</param>
        void buildController(Model model, IView view);
    }
}
