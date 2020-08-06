using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
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

        private readonly FirestoreDb db;
        private readonly CollectionReference highScoresRef;

        private HighScores()
        {
            var client = new FirestoreClientBuilder();
            client.JsonCredentials = "";
            db = FirestoreDb.Create(Properties.Resources.FirebaseProject, client.Build());
            highScoresRef = db.Collection("high-scores");
        }

        async public Task<IEnumerable<HighScore>> GetHighScores()
        {
            var highScores = await highScoresRef.OrderByDescending("score").Limit(10).GetSnapshotAsync();
            return highScores.Select(s => new HighScore {
                Name = s.GetValue<string>("name"),
                Score = s.GetValue<int>("score"),
            });
        }

        public class HighScore
        {
            public string Name;
            public int Score;
        }
    }
}
