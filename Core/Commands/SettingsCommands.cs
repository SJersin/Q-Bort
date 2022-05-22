using System;
using System.Timers;
using System.Collections.Generic;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Linq;
using QBort.Core.Database;

namespace QBort.Core.Commands
{
    public class SettingsCommands : ModuleBase<SocketCommandContext>
    {
        [Command("set-ochan")]
        [Summary(": Sets the channel ID for a Guild specifying the channel they would like to use the `open` command exclusively in.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task SetQueueChannel()
        {
            await Context.Channel.TriggerTypingAsync();
            if (Guild.SetQueueMessageRoom(Context.Guild.Id, Context.Channel.Id) == 1)
                await Context.Channel.SendMessageAsync("Queue channel set.");
            else
                await Context.Channel.SendMessageAsync("The channel could not be set.");
        }
        [Command("set-pchan")]
        [Summary(": Sets the channel ID for a Guild specifying the channel they would like to use the `new` command exclusively in.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task SetPullChannel()
        {
            if (Guild.SetPullMessageRoom(Context.Guild.Id, Context.Channel.Id) == 1)
                await Context.Channel.SendMessageAsync("Pull channel set.");
            else
                await Context.Channel.SendMessageAsync("The channel could not be set.");
        }

        [Command("set-react")]
        [Summary(": sets the reaction for users to react to.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task SetGuildReact(string repeat)
        {
            if (Guild.SetReaction(Context.Guild.Id, repeat) == 1)
                await Context.Channel.SendMessageAsync("Reaction set.");
            else
                await Context.Channel.SendMessageAsync("The reaction could not be set.");
        }

        [Command("set-role")]
        [Summary(": sets the role the bot will the bot will check that users have.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task SetGuildRole(IRole role)
        {
            if (Guild.SetRole(Context.Guild.Id, role.Name) == 1)
                await Context.Channel.SendMessageAsync("Role set.");
            else
                await Context.Channel.SendMessageAsync("The role could not be set.");
        }

        [Command("game")]
        [Alias("Game")]
        [Summary(": Sets the game for which will the queue will be playing.\nex: `game Paladins`")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task SetGame([Remainder] string game = "")
        {
            await Context.Channel.SendMessageAsync("This isn't working yet.\nError Code: Dev404");
        }

        [Command("mode")]
        [Alias("Mode")]
        [Summary(": Sets the mode or game type for the game for which will the queue will be playing.\nex: `mode Team Death Match`")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task SetGameMode([Remainder] string game = "")
        {
            await Context.Channel.SendMessageAsync("This isn't working yet.\nError Code: Dev404");
        }
    }
}
