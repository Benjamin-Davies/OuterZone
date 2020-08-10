using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OuterZone
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            await HighScores.DefaultInstance.SubmitScore(new HighScore
            {
                Username = "Anonymous",
                Score = new Random().Next(1000),
            });

            var highScores = await HighScores.DefaultInstance.GetHighScores();
            foreach (var s in highScores)
                Console.WriteLine($"{s.Username}: {s.Score}");

            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new MainWindow());
        }
    }
}
