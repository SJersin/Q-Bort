using System;

namespace QBort
{
    public class MapList
    {
        /*
            TODO Make this read files from a directory to load map names into database.
            Formatting could be:
            [gamename].txt  e.g. paladins.txt

            Id          -   Primary Key, Auto-increment
            GameName    -   filename.ProperCase or whatever.
            GameMode    -   Specific game mode the map is only played in, if it truly is what it is.
        */
        public static string[] List { get; set; }
        private static string[] Paladins = { "Frog Isle", "Jaguar Falls", "Serpent Beach", "Frozen Guard", "Ice Mines", "Fish Market", "Timber Mill", "Stone Keep", "Brightmarsh", "Splitstone Quarry", "Ascension Peak", "Warder's Gate", "Shattered Desert", "Bazaar"};
        private static string[] PalGameModes = { "Siege", "Onslaught", "King of the hill", "Team Deathmatch", "Ranked" };

        // TODO Finish this eventually.
        public static void CreateOrLoadList()
        {
            string file_loc = ".data/map_list.txt";
            try
            {
                string x = "# Add map names on their own line:" +
                    "\n# Example:\n# Stone Keep\n# de_dust\n# ";
                if (System.IO.File.Exists(file_loc))
                {
                    List = System.IO.File.ReadAllLines(file_loc);
                }
                else
                {
                    System.IO.File.AppendAllText(file_loc, x);
                    Console.WriteLine("\nA new Map_list file has been created in the Resources directory. Don't forget to add map names to it!");
                }
                    
            }
            catch (Exception e)
            {
                Log.Error("Could not read or create Map_List file.");
                Log.Error($"[{Messages.DateTimeStamp()} {e.InnerException}] {e.Source}:\n{e.Message}\n{e.StackTrace}");
            }

        }
    }
}



 
