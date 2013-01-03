using System;

namespace ISL.Server
{
    public enum WebsocketOpCode
    {
        Continuation = 0x0,
        Text         = 0x1,
        Binary       = 0x2,
        Reserved3    = 0x3,
        Reserved4    = 0x4,
        Reserved5    = 0x5,
        Reserved6    = 0x6,
        Reserved7    = 0x7,
        Close        = 0x8,
        Ping         = 0x9,
        Pong         = 0xA,
        ReservedB    = 0xB,
        ReservedC    = 0xC,
        ReservedD    = 0xD,
        ReservedE    = 0xE,
        ReservedF    = 0xF
    }
}

