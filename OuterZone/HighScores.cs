using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterZone
{
    class HighScores
    {
        static private HighScores defaultInstance = null;
        static public HighScores DefaultInstance
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new HighScores();
                }
                return defaultInstance;
            }
        }

        private HighScores()
        {
        }

        async public Task<IEnumerable<HighScore>> GetHighScores()
        {
            return null;
        }

        public class HighScore
        {
            public string Name;
            public int Score;
        }
    }
}
