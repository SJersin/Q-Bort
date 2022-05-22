using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace QBort
{
    class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _Services;

        // Constructor
        //  public CommandManager(DiscordSocketClient Client, CommandService Commands) Passing the client socket and command service without using IServiceProvider
        public CommandHandler(IServiceProvider Services)
        {
            //_Client = Client;
            _client = Services.GetRequiredService<DiscordSocketClient>();
            //_Commands = Commands;
            _commands = Services.GetRequiredService<CommandService>();
            _Services = Services;
        }
        public async Task InitializeAsync()
        {
            Console.WriteLine("Initializing command assembly list..."); // DEBUG
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _Services); //Second arguement set IServices. Use null if not using an IService.
            foreach (var cmd in _commands.Commands)
                Console.WriteLine($"{DateTime.Now} => [COMMANDS] : {cmd.Name} was loaded.");

            _commands.Log += Command_Log;
        }
        private Task Command_Log(LogMessage Command)
        {
            Console.WriteLine(Command.Message);
            return Task.CompletedTask;
        }
    }
}
