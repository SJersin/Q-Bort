/*
    This is the Guild Settings class for modifying guild settings.

    For retrieving Guild Settings, see the Guild class.

    Table Schema:
      GuildSettings - 
        GuildId INTEGER,            Primary Key
        GroupSize INTEGER,          Default number of users to pull from active list.
        MaxGroupSize INTEGER,       Maximum number of users to pull from active list.
        BotPrefix VARCHAR(5),       Character(s) used for the bot to respond to. Should allow per server customization.
        Reaction VARCHAR(50),       Unicode or Discord unique name for the reaction to agree to server terms. Should allow per server customization.
        QueMsgRoom INTEGER,         A specified room Id for the "open" command. If set, will only allow "open" to used in the specified room.
        PullMsgRoom INTEGER,        A specified room Id for the "new" command. If set, will only allow "new" to used in the specified room.
        ModSettingsRoom INTEGER,    A specified room Id for mod commands. If set, will only allow mod commands to used in the specified room.
        UserSettingsRoom INTEGER,   A specified room Id for user commands. If set, will only allow user commands to used in the specified room.
        QueMsgId INTEGER,           The id of the message that is created and sent by the bot when the "open" command is used.
        PullMsgId INTEGER,          The id of the message that is created and sent by the bot when the "new" command is used.
        Reaction VARCHAR(50),      Name of the role to give users when they react to the server terms message
        SubLv INTEGER               If I ever feel to incorporate subscription tiers. Capitalism, Ho!

      Guilds - 
        GuildId INTEGER,            Primary Key 
        IsOpen INTEGER,             Boolean value for the status of the guilds queue. 0 for false (queue closed), 1 for true (queue open).
        GameName VARCHAR(50),       String value for the name of the game the queue will be hosting.
        GameMode VARCHAR(50)        String value for the name of the game mode the next pull will be using.

      Players - 
        GuildId INTEGER,            Primary Key
        PlayerId INTEGER,           Primary Key
        Nickname VARCHAR(50),       Supposed to be the user's in-game name... We'll see.
        PlayCount INTEGER,          The number of games the user has played.
        IsActive INTEGER,           Boolean value for the active status of the user.  
        SpecialGames INTEGER,       Boolean value to indicate if the user want to participate in "special rules" games.
        IsBanned INTEGER,           Boolean value for the banned status of the user.
        BanReason VARCHAR(500),     Reason for the banning.
        Agreed INTEGER              Boolean value for the agrement
*/
namespace QBort.Core.Database
{
    internal class Settings
    {
        internal static int SetGroupSize(ulong GuildId, string set)
        {
            string query = $"UPDATE Guild SET GroupSize = {set} WHERE GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
        internal static int SetMaxGroupSize(ulong GuildId, string set)
        {
            string query = $"UPDATE Guild SET MaxGroupSize = {set} WHERE GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
        internal static int SetPrefix(ulong GuildId, string set)
        {
            string query = $"UPDATE Guild SET Prefix = {set} WHERE GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
        internal static int SetReaction(ulong GuildId, string set)
        {
            string query = $"UPDATE Guild SET Reaction = {set} WHERE GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
        internal static int SetQueueMessageRoom(ulong GuildId, string set)
        {
            string query = $"UPDATE Guild SET QueueMessageRoom = {set} WHERE GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
        internal static int SetPullMessageRoom(ulong GuildId, string set)
        {
            string query = $"UPDATE Guild SET PullMessageRoom = {set} WHERE GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
        internal static int SetModSettingsRoom(ulong GuildId, string set)
        {
            string query = $"UPDATE Guild SET ModSettingsRoom = {set} WHERE GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
        internal static int SetUserSettingsRoom(ulong GuildId, string set)
        {
            string query = $"UPDATE Guild SET UserSettingsRoom = {set} WHERE GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
        internal static int SetCustomRole(ulong GuildId, string set)
        {
            string query = $"UPDATE Guild SET CustomRole = {set} WHERE GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
    }
}