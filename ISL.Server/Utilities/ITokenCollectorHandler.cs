using System;
using ISL.Server.Network;

namespace ISL.Server
{
    public interface ITokenCollectorHandler
    {
        void deletePendingClient(NetComputer client);
        void deletePendingConnect(object data);
        void tokenMatched(NetComputer computer, object data);
    }
}

