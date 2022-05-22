using System;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Linq;
using Discord.WebSocket;
using QBort.Core.Database;

/*
 0.10.1

    The Great Refactoring happened.

 0.8.3
 
 Changed Recall to accept a message from the mod. or nothing at all with no message.
 
 
 
 0.6c - Changes to file:
 
 - Added qstats command.
 
*/


namespace QBort.Core.Commands
{
    public class ModCommands : ModuleBase<SocketCommandContext>
    {   
        private SocketGuildUser _user = null;

        [Command("ban")]
        [Summary(": Ban a player from the queue\nCan pass a reason as a second argument.\nCan use either @mention or Discord ID\nex. `ban 123456789 reason.`")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task BanPlayer(string userID, [Remainder] string reason = "")
        {

            await Context.Channel.TriggerTypingAsync();
            var embed = new EmbedBuilder().WithTitle("Ban Player");

            try  //Check if userID is an @mention or a discordID and assigns them appropriately.
            {
                _user = Context.Guild.GetUser(ulong.Parse(userID));
            }
            catch //Get Mentioned user
            {
                _user = Context.Guild.GetUser(Context.Message.MentionedUsers.First().Id);
            }

            if (reason == "")
                reason = $"{DateTime.Now} {Context.User.Username}: Reason left empty.";

            if (_user is not null)
            {
                if (0 > Guild.BanPlayer(Context.Guild.Id, _user.Id))
                {
                   embed.WithDescription($"{_user.Username} has been banned from the queue.\nReason: {reason}")
                     .WithColor(Color.DarkRed);
                   await _user.SendMessageAsync($"You have been banned from the queue by {Context.User.Username}.\nReason: {reason}");
                } else {

                }
            } else
                embed.WithDescription("Player not found.")
                  .WithColor(Color.DarkRed); 
            
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("unban")]
        [Summary(": Removes a player from the banned list. Can use either @mention or Discord ID.")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task UnbanPlayer(string userID)
        {
            var embed = new EmbedBuilder().WithTitle("Unban Player");

            try  //Check if userID is an @mention or a discordID and assigns them appropriately.
            {
                _user = Context.Guild.GetUser(ulong.Parse(userID));
            }
            catch //Get Mentioned user
            {
                _user = Context.Guild.GetUser(Context.Message.MentionedUsers.First().Id);
            }

            if (_user is not null)
            {
                Guild.UnbanPlayer(Context.Guild.Id, _user.Id);
                embed.WithDescription($"The ban on {_user.Username} has been lifted.")
                  .WithColor(Color.DarkRed);
                await _user.SendMessageAsync($"Your ban from {Context.Guild.Name} has been lifted by {Context.User.Username}.");
            } else
                embed.WithDescription("Player not found")
                  .WithColor(Color.DarkRed); 
            
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("gp+")]
        [Summary(": Adds one to the games played counter of provided user\nAccepts either @mentions or User IDs.")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task IncreasePlayCount([Remainder]string userID)
        {
            if (!Guild.GetLobbyStatus(Context.Guild.Id))
            {
                await Context.Channel.SendMessageAsync(Messages.LobbyIsClosed);
                return;
            }
            // TODO Add foreach to allow multiple users to be passed.

            await Context.Channel.TriggerTypingAsync();

            try  //Check if userID is an @mention or a discordID and assigns them appropriately.
            {
                if (ulong.TryParse(userID, out ulong _id))
                    _user = Context.Guild.GetUser(_id);
                else
                    _user = Context.Guild.GetUser(Context.Message.MentionedUsers.FirstOrDefault().Id);
            }
            catch (Exception e)
            {
                Log.Error($"[{Messages.DateTimeStamp()} {e.InnerException}] {e.Source}:\n{e.Message}\n{e.StackTrace}");
                await Context.Channel.SendMessageAsync("Nope. Try again.");
                return;
            }
            int result = Player.IncreasePlayCount(_user.Id, Context.Guild.Id);

            if  ( result == 1)
                await Context.Channel.SendMessageAsync($"Game count for {_user.Username} has been decreased.");
            else if (result == 0)
                await Context.Channel.SendMessageAsync($"User not found?");

        }

        [Command("gp-")]
        [Summary(": Subtracts one from the games played counter of provided user\nAccepts either @mentions or User IDs.")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task DecreasePlayCount([Remainder]string userID)
        {
            if (!Guild.GetLobbyStatus(Context.Guild.Id))
            {
                await Context.Channel.SendMessageAsync(Messages.LobbyIsClosed);
                return;
            }

            await Context.Channel.TriggerTypingAsync();
            ulong _id;

            try  //Check if userID is an @mention or a discordID and assigns them appropriately.
            {
                _id = ulong.Parse(userID);
                _user = Context.Guild.GetUser(_id);
            }
            catch //Get mentioned user
            {
                _user = Context.Guild.GetUser(Context.Message.MentionedUsers.First().Id);
            }
            int result = Player.DecreasePlayCount(_user.Id, Context.Guild.Id);

            if  ( result == 1)
                await Context.Channel.SendMessageAsync($"Game count for {_user.Username} has been decreased.");
            else if (result == 0)
                await Context.Channel.SendMessageAsync($"Nothing was changed. User not found?");

        }

/*
        [Command("test")]
        [Summary(": For testing code purposes. Don't actually use this without express permission.\nCurrently registers a guild with the database.")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task testing()
        {
            //Template
        }
*/

        [Command("test")]
        [Summary(": For testing code purposes. Don't actually use this without express permission.\nCurrently registers a guild with the database.")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task testing()
        {
            string check = Database.Database.RegisterGuild(Context.Guild.Id);
            switch (check)
            {
                case "added":     
                    await Context.Channel.SendMessageAsync("Guild has been successfully registered.");
                    break;
            
                case "exists":
                    await Context.Channel.SendMessageAsync("The guild id already exists.");
                    break;

                default:
                    await Context.Channel.SendMessageAsync("There was an error registering the guild: " + check);
                    break;
            }
        }
    }
}
