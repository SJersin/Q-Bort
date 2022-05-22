/*
    This class is only for creating the tables in the database.

    Table Schema:
      GuildSettings - 
        GuildId INTEGER             Primary Key
        GroupSize INTEGER           Default number of users to pull from active list.
        MaxGroupSize INTEGER        Maximum number of users to pull from active list.
        BotPrefix VARCHAR(5)        Character(s) used for the bot to respond to. Should allow per server customization.
        Reaction VARCHAR(50)        Unicode or Discord unique name for the reaction to agree to server terms. Should allow per server customization.
        QueMsgRoom INTEGER          A specified room Id for the "open" command. If set, will only allow "open" to used in the specified room.
        PullMsgRoom INTEGER         A specified room Id for the "new" command. If set, will only allow "new" to used in the specified room.
        ModSettingsRoom INTEGER     A specified room Id for mod commands. If set, will only allow mod commands to used in the specified room.
        UserSettingsRoom INTEGER    A specified room Id for user commands. If set, will only allow user commands to used in the specified room.
        QueMsgId INTEGER            The id of the message that is created and sent by the bot when the "open" command is used.
        PullMsgId INTEGER           The id of the message that is created and sent by the bot when the "new" command is used.
        Reaction VARCHAR(50)       Name of the role to give users when they react to the server terms message
        SubLv INTEGER               If I ever feel to incorporate subscription tiers. Capitalism, Ho!

      Guilds - 
        GuildId INTEGER             Primary Key 
        IsOpen INTEGER              Boolean value for the status of the guilds queue. 0 for false (queue closed), 1 for true (queue open).
        GameName VARCHAR(50)        String value for the name of the game the queue will be hosting.
        GameMode VARCHAR(50)        String value for the name of the game mode the next pull will be using.
        RecallGroup VARCHAR(100)

      Players - 
        GuildId INTEGER,            Primary Key
        PlayerId INTEGER,           Primary Key
        PlayCount INTEGER,          The number of games the user has played.
        IsActive INTEGER,           Boolean value for the active status of the user.  
        SpecialGames INTEGER,       Boolean value to indicate if the user want to participate in "special rules" games.
        IsBanned INTEGER,           Boolean value for the banned status of the user.
        BanReason VARCHAR(500),     Reason for the banning.
        Agreed INTEGER              Boolean value for the agrement
*/


namespace QBort.Core.Database
{
     internal class Tables
     {
        internal static void CreateGuildsTable()
        {
            // Sanitize Strings
            // TODO Learn sanitization practices/techniques
            string tquery = "CREATE TABLE Guilds(GuildId INTEGER PRIMARY KEY, IsOpen INTEGER, GameName VARCHAR(100), GameMode VARCHAR(100), RecallGroup VARCHAR(100), IsActive INTEGER)";

            int value = Database.ExecuteWrite(tquery);
            // Check for table creation success

            // Create a default value for table and check to make sure it succeeded.
            int seed = Guild.AddGuild(0);
            if (seed != 1)
            {
                //TODO Something went wrong creating the table
            }
        }

        internal static void CreatePlayersTable()
        {
            // Sanitize Strings
            // TODO Learn sanitization practices/techniques
            string tquery = "CREATE TABLE Players(GuildId INTEGER, PlayerId INTEGER, PlayCount INTEGER"
                + ", IsActive INTEGER, SpecialGames INTEGER, IsBanned INTEGER, BanReason VARCHAR(500), Agreed INTEGER)";

            int value = Database.ExecuteWrite(tquery);
            if (value != 1)
            {
                //TODO Something went wrong creating the table
            }

            // Create a default value for table and check to make sure it succeeded.
            int seed = Player.AddPlayer(0, 0);
            if (seed != 1)
            {
                //TODO Something went wrong seeding the table
            }           
        }

        internal static void CreateGuildSettingsTable()
        {
            string tquery = "CREATE TABLE GuildSettings(GuildId INTEGER, GroupSize INTEGER, MaxGroupSize INTEGER, BotPrefix VARCHAR(5), Reaction VARCHAR(50), QueMsgRoom INTEGER, "
            + "PullMsgRoom INTEGER, ModSettingsRoom INTEGER, UserSettingsRoom INTEGER, QueMsgId INTEGER, PullMsgId INTEGER, Role VARCHAR(20), SubLv INTEGER)";

            int value = Database.ExecuteWrite(tquery);
            if (value != 1)
            {
                //TODO Something went wrong creating the table
            }

            // Create a default value for table and check to make sure it succeeded.
            int seed = Guild.AddGuildSettings(0);
            if (seed != 1)
            {
                //TODO Something went wrong seeding the table
            }
        }
    }
}