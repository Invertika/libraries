using System;

namespace ISL.Server
{
    public class TokenItem
    {
        string token; /**< Cookie used by the client. */
        //intptr_t data;     /**< User data. */
        DateTime timeStamp;  /**< Creation time. */

        public TokenItem()
        {
        }
    }
}

