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
        public readonly static RosNode node = new RosNode("10.2.3.19", 8878);
    }
}
