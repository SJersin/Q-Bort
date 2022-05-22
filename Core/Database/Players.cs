/*
    This class holds the methods for the various sql queries
    to get and change data reguarding "Players"


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
        RecallGroup VARCHAR(100)

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

using System;
using System.Collections.Generic;
using System.Data;

namespace QBort.Core.Database
{
    internal class Player
    {
        internal ulong PlayerId { get; }
        internal int PlayCount { get; }

        internal Player(ulong id, int c)
        {
            PlayerId = id;
            PlayCount = c;
        }



        // *********** STATIC FUNCTIONS ***********************************
        internal static int AddPlayer(ulong GuildId, ulong PlayerId)

        {
            const string query = "INSERT INTO Players(GuildId, PlayerId, PlayCount, IsActive, SpecialGames, IsBanned, BanReason, Agreed) VALUES(@GuildId, @PlayerId, "
                + "@PlayCount, @IsActive, @SpecialGames, @IsBanned, @BanReason, @Agreed)";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                // TODO Double check the default IsActive setting before Finalizing Release. 
                { "@GuildId", GuildId },
                { "@PlayerId", PlayerId },
                { "@PlayCount", 0 },
                { "@IsActive", 0 },
                { "@SpecialGames", 0 },
                { "@IsBanned", 0 },
                { "@BanReason", "Nothing yet." },
                { "@Agreed", 0 }
            };

            return Database.ExecuteWrite(query, args);
        }
        internal static int DecreasePlayCount(ulong GuildId, ulong PlayerId)
        {
            var dt = Database.ExecuteRead($"SELECT PlayCount FROM Players WHERE GuildId = {GuildId} AND PlayerId = {PlayerId}");
            int current = Convert.ToInt16(dt.Rows[0]["PlayCount"]);
            int newvalue = current - 1;

            string query = $"UPDATE Players SET PlayCount = {newvalue} WHERE GuildId = {GuildId} AND PlayerId = {PlayerId}";
            return Database.ExecuteWrite(query);
        }
        internal static int EditPlayerData(ulong GuildId, ulong PlayerId, string setting, string value)
        {
            // TODO Sanitize this string
            string query = $"UPDATE Players SET {setting} = {value} WHERE GuildId = {GuildId} AND PlayerId = {PlayerId}";

            return Database.ExecuteWrite(query);
        }
        internal static bool Exists(ulong GuildId, ulong PlayerId)
        {
            try
            {
                string query = $"SELECT * FROM Players Where GuildId = {GuildId} AND PlayerId = {PlayerId}";
                if (Database.ExecuteRead(query).Rows.Count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                Log.Error($"[{Messages.DateTimeStamp()} {e.InnerException}] {e.Source}:\n{e.Message}\n{e.StackTrace}"); return true;
            }
        }
        internal static int GetActiveStatus(ulong GuildId, ulong PlayerId)
        {

            string query = $"SELECT * FROM Players WHERE GuildId = {GuildId} AND PlayerId = {PlayerId}";
            var dt = Database.ExecuteRead(query);

            try
            {
                return Convert.ToUInt16(dt.Rows[0]["IsActive"]);
            }
            catch (Exception e)
            {
                Log.Error($"[{Messages.DateTimeStamp()} {e.InnerException}] {e.Source}:\n{e.Message}\n{e.StackTrace}");
                return -1;
            }
        }
        internal static int GetPlayCount(ulong GuildId, ulong PlayerId)
        {
            string query = $"SELECT PlayCount FROM Players WHERE PlayerId = {PlayerId} AND GuildId = {GuildId}";
            var dt = Database.ExecuteRead(query);

            var PlayCount = Convert.ToUInt16(dt.Rows[0]["PlayCount"]);
            return PlayCount;
        }
        internal static int IncreasePlayCount(ulong GuildId, ulong PlayerId)
        {
            var dt = Database.ExecuteRead($"SELECT * FROM Players WHERE GuildId = {GuildId} AND PlayerId = {PlayerId}");
            int current = Convert.ToInt16(dt.Rows[0]["PlayCount"]);
            int _n = current + 1;
            string query = $"UPDATE Players SET PlayCount = {_n} WHERE GuildId = {GuildId} AND PlayerId = {PlayerId}";
            return Database.ExecuteWrite(query);
        }
        internal static int ChangeActiveStatus(ulong GuildId, ulong PlayerId)
        {
            string query = $"SELECT IsActive FROM Players WHERE PlayerId = {PlayerId} AND GuildId = {GuildId}";
            var dt = Database.ExecuteRead(query);
            if (dt.Rows.Count == 0) return -1;

            var status = Convert.ToUInt16(dt.Rows[0]["IsActive"]);
            int value;
            if (status != 0) value = 0; else value = 1;

            query = string.Empty;
            query = $"UPDATE Players SET IsActive = {value} WHERE PlayerId = {PlayerId} AND GuildId = {GuildId}";
            return Database.ExecuteWrite(query);
        }
        internal static DataTable GetConfirmedPlayers(ulong GuildId)
        {
            return Database.ExecuteRead("SELECT * FROM Players WHERE GuildId = " + GuildId + " WHERE Agreed = 1");
        }
    }

}