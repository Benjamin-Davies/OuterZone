using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OuterZone.Properties;

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

        private readonly HttpClient httpClient;
        private string databaseUrl = $"https://firestore.googleapis.com/v1/projects/{Resources.FirebaseProject}/databases/(default)";

        private HighScores()
        {
            httpClient = new HttpClient();
        }

        async public Task<List<HighScore>> GetHighScores()
        {
            var highScoresJson = await httpClient.GetStringAsync($"{databaseUrl}/documents/high-scores");
            var highScores = JsonConvert.DeserializeObject<DocumentsList<HighScoreDocument>>(highScoresJson);
            return highScores.Documents.Select(s => (HighScore) s).ToList();
        }

#region Api Classes

        private class DocumentsList<T>
        {
            public List<T> Documents { get; set; }
        }

        private class StringField
        {
            public string StringValue { get; set; }

            public static implicit operator string(StringField field)
                => field.StringValue;
        }

        private class IntegerField
        {
            public int IntegerValue { get; set; }

            public static implicit operator int(IntegerField field)
                => field.IntegerValue;
        }

        private class HighScoreDocument
        {
            public HighScoreFields Fields { get; set; }

            public class HighScoreFields
            {
                public StringField Username { get; set; }
                public IntegerField Score { get; set; }
            } 

            public static explicit operator HighScore(HighScoreDocument doc)
                => new HighScore
                    {
                        Username = doc.Fields.Username,
                        Score = doc.Fields.Score,
                    };
        }

#endregion

        public class HighScore
        {
            public string Username;
            public int Score;
        }
    }
}
