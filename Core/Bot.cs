/*
 * A player queue management bot originally designed for KamiVS weekly customs games
 * in the Hi-Rez team based hero shooter game, Paladins Champions of the Realm.
 * Will manage a large group of users in a list style with
 * functions to pull however many players you need for the next game lobby.
 * 
 * Has been designed so that arguments can be passed to accomidate other games such as
 * Overwatch, CS:GO, Call of Duty, or pretty much any first person shooter game that
 * has custom matches that can be made private.
 * 
 * Do not forget to configure the bot in the \Resources\config.json file!
 * 
 * By: Jersin - 12 DEC 2020
 */

using System;
using System.Threading.Tasks; //Run Async tasks
using Discord;
using Discord.Commands; //Discord command handler
using Discord.WebSocket; //Discord Web connection
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.IO;
using QBort.Core.Database;
using Serilog.Events;

namespace QBort
{
    public class Bot
    {
        private readonly DiscordSocketClient _client;    // Socket Client for things
        private readonly CommandService _commands;       // Command Services
        private readonly IServiceProvider _services;     // Interface Service Provider 

        public Bot()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 10000, // TODO find out how large the message cache size can go. Might be the reason the Close command doesn't work.
                GatewayIntents = GatewayIntents.AllUnprivileged // Read up on GatewayIntents in documentation.
            });

            _commands = new CommandService(new CommandServiceConfig
            {
                // Make bot respond to case sensitive commands, defualt to running asynchronously, and set log level.
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            _services = BuildServiceProvider();
        }

        public async Task MainAsync()
        {
            //--Initialize CommandManager
            // CommandManager cmdManager = new CommandManager(Services);
            // await cmdManager.InitializeAsync();

            //--Alternative, easier way to initialize CommandManager.
            await new CommandHandler(_services).InitializeAsync();
            await new EventHandler(_services).InitializeAsync();

            _client.Log += Client_Log;

            Console.WriteLine("Checking Database...");
            Database.CheckDatabase();

            Console.WriteLine($"{DateTime.Now} => [Default Prefix Key] : Set to [+].");
            try
            {
                await _client.LoginAsync(TokenType.Bot, Token.GetToken());
            }
            catch (Exception e)
            {
                Log.Error($"[{Messages.DateTimeStamp()} {e.InnerException}] {e.Source}:\n{e.Message}\n{e.StackTrace}");
            }
            await _client.StartAsync();
            await Task.Delay(Timeout.Infinite); //Time for tasks to run. -1 is unlimited time. Timeout.Infinite has clearer intent.

        }

        private Task Client_Log(LogMessage message)
        {
            //Consistancy is important I guess.

            // Filter out annoying repetative messages.
            if (message.Message != "Received Dispatch (PRESENCE_UPDATE)")
                if (message.Message != "Received Dispatch (TYPING_START)")
                    if (message.Message != "Received Dispatch (MESSAGE_CREATE)")    // Too lazy for &&
                        if (message.Message != "Received HeartbeatAck")
                            if (message.Message != "Sent Heartbeat")
                                Console.WriteLine($"{DateTime.Now} => [{message.Source}] : {message.Message}");

            var severity = message.Severity switch
            {
                LogSeverity.Critical => LogEventLevel.Fatal,
                LogSeverity.Error => LogEventLevel.Error,
                LogSeverity.Warning => LogEventLevel.Warning,
                LogSeverity.Info => LogEventLevel.Information,
                LogSeverity.Verbose => LogEventLevel.Verbose,
                LogSeverity.Debug => LogEventLevel.Debug,
                _ => LogEventLevel.Information
            };
            Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);

            return Task.CompletedTask;
        }

        private ServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
        }
    }
}

