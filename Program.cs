/*
    Q-Bort - A queue management bot designed for use in multiple guilds.
    
    By: Jersin - 12 DEC 2020
    
 */

global using Serilog;
using System;
using Serilog.Events;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace QBort
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .WriteTo.File("logs/qbort_log.txt")
               .CreateLogger();

            try
            {
                Bot QBort = new Bot();
                Console.WriteLine("Starting bot...");
                QBort.MainAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Log.Fatal($"Bot could not start:");
                Log.Error($"[{Messages.DateTimeStamp()} {e.InnerException}] {e.Source}:\n{e.Message}\n{e.StackTrace}");
            }
        }
    }
}
