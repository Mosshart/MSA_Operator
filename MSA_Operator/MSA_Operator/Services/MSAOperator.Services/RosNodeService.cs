using RosCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAOperator.Services
{
    public class RosNodeService
    {
        private RosNode _node;
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
            
            // = new RosNode("10.2.3.19", 8878);
        public bool isConnected = false;

        public void ChangeNodeConnected(string IpAddress, int PORT = 8878)
        {
            node = new RosNode(IpAddress, PORT);
        }

    }
}
//public RosNode node;

//public string _connectionString;
//public string ConnectionString
//{
//    private get => _connectionString;
//    set
//    {
//        try
//        {
//            if (node != null)
//                node.Dispose();

//            node = new RosNode(value, 8878);

//        }
//        catch (Exception e) { }
//    }
//}
//public void ChangeNodeConnection(string newIpAddress)
//{

//}