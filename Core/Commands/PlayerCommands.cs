using System;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using QBort.Core.Database;


/*
 * 0.10 Changes:
 * 0.10.1
 * - Refactored code. Hopefully a bit more efficient now.
    New player commands:
        Done: Changes your status to inactive, effectively removing you from the queue.\nEx. ` done `
        Join: Joins the current active queue and setting you as active. \nex: ` join `
        Modes: Indicate you would like to participate in special game modes. This setting is defaulted to off. The bot will not pull players who do not wish to participate in those game modes. \nEx. ` modes `
 * 
 * 0.8 changes made:
 * 8.4.6
 * - Status: changed back to toggling between active and inactive status.
 * 
 * 8.0.0 
 * - Clarified intent and usage of the "Quit" command.
 * - "Join" command put on indefinite hiatus. 
 * 
 * 0.6 - Changes to file:
 * 
 * -Status: no longer takes arguements. Instead toggles between active and inactive states.
 * -Join: Start work on command to allow players to join the queue without having to click the
 *     reaction emote. Why? Why not? Will need to find out if desired. But until then, still doing it.
 * -Remove certain player commands to check stability.
 * 
 */

namespace QBort.Core.Commands
{
    // public class PlayerCommands : ModuleBase<SocketCommandContext>
    // {
    //     [Command("modes")]
    //     [Alias("mode")]
    //     [Summary(": Indicate you would like to participate in special game modes. This setting is defaulted to off. The bot will not pull players who do not wish to participate in those game modes.")]
    //     public async Task SetGameModes(string modes = "")
    //     {
    //         // TODO Work on player game mode setting

    //         await Task.CompletedTask;
    //     }
    // }
}
