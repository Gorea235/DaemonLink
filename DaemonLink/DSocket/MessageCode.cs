using System;

namespace DaemonLink.DSocket
{
    enum MessageCode : byte
    {
        // Communication messages

        /// <summary>
        /// A special value that states no message code will be sent, just the
        /// content.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// States that the client is sending the arguments from the
        /// command line.
        /// </summary>
        Arguments = 0x01,
        /// <summary>
        /// States that the client should write the next string to StdOut.
        /// </summary>
        Write = 0x10,
        /// <summary>
        /// States that the client should flush StdOut.
        /// </summary>
        Flush = 0x11,
        /// <summary>
        /// States that the client should write the next string to StdErr.
        /// </summary>
        WriteError = 0x18,
        /// <summary>
        /// States that the client should flush StdErr.
        /// </summary>
        FlushError = 0x19,
        /// <summary>
        /// States that the client should read from StdIn.
        /// </summary>
        ReadChar = 0x21,

        // Control messages
        /// <summary>
        /// States that the server has finished processing the arguments and
        /// that it has sent the status code.
        /// </summary>
        Finished = 0xf0,
        /// <summary>
        /// States that the server had an unexpected error and crashed.
        /// </summary>
        Error = 0xf6
    }
}
