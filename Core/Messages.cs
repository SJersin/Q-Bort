using System;

namespace QBort
{
    class Messages
    {
        public const string LobbyIsClosed  = "There is no open q-υωυ-e, silly.";
        public const string LobbyIsOpen  = "The q-υωυ-e is already open, silly.";
        public const string LowActivePlayerWarning = "The active player count is below the set group size.";
        public const string WrongChannelWarning = "This is the wrong channel for this command.";
        internal static string DateTimeStamp(){ return $"[{DateTime.Now.ToLongDateString()} | {DateTime.Now.ToLongTimeString()}]";}
    }
}