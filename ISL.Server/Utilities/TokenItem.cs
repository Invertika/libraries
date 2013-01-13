using System;

namespace ISL.Server
{
    public class TokenItem
    {
        public string Token { get; private set; } /**< Cookie used by the client. */
        public object Data { get; private set; }     /**< User data. */
        public DateTime TimeStamp { get; private set; }  /**< Creation time. */

        public TokenItem(string token, object data, DateTime timeStamp)
        {
            Token=token;
            Data=data;
            TimeStamp=timeStamp;
        }
    }
}

