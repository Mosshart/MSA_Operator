using RosCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MSAOperator.Services
{
    public class RosNodeService
    {
        private RosNode _node;
        /// <summary>
        /// get/set communication node object
        /// </summary>
        public RosNode node
        {
            get
            {
               
                return _node;
            }
            private set
            {
                try
                {
                    _node = value;
                    if (_node == null)
                        _node = new RosNode("1.1.1.1",8878);
                }
                catch (Exception e) { }
            }
        }
            
        /// <summary>
        /// connection status
        /// </summary>
        public bool isConnected = false;

        /// <summary>
        /// update node,
        /// </summary>
        /// <param name="IpAddress">ip address of device of connect</param>
        /// <param name="PORT">network port, defaul 8878</param>
        public void ChangeNodeConnected(string IpAddress, int PORT = 8878)
        {
            node = new RosNode(IpAddress, PORT);
        }

    }
}