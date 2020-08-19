using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        private readonly JsonSerializerSettings jsonSettings;
        private string databaseUrl = $"https://firestore.googleapis.com/v1/projects/{Resources.FirebaseProject}/databases/(default)";

        private HighScores()
        {
            httpClient = new HttpClient();
            jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(),
                },
            };
        }

        async public Task<List<HighScore>> GetHighScores()
        {
            var highScoresJson = await httpClient.GetStringAsync($"{databaseUrl}/documents/high-scores?orderBy=score desc");
            var highScores = JsonConvert.DeserializeObject<DocumentsList<HighScoreDocument>>(highScoresJson, jsonSettings);
            return highScores.Documents.Select(s => (HighScore) s).ToList();
        }

        async public Task SubmitScore(HighScore score)
        {
            var scoreDocument = (HighScoreDocument) score;
            var scoreJson = JsonConvert.SerializeObject(scoreDocument, jsonSettings);
            var content = new StringContent(scoreJson, Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync($"{databaseUrl}/documents/high-scores", content);
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
            public static implicit operator StringField(string value)
                => new StringField { StringValue = value };
        }

        private class IntegerField
        {
            public int IntegerValue { get; set; }

            public static implicit operator int(IntegerField field)
                => field.IntegerValue;
            public static implicit operator IntegerField(int value)
                => new IntegerField { IntegerValue = value };
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

            public static explicit operator HighScoreDocument(HighScore doc)
                => new HighScoreDocument
                    {
                        Fields = new HighScoreDocument.HighScoreFields
                        {
                            Username = doc.Username,
                            Score = doc.Score,
                        },
                    };
        }

#endregion
    }

    public class HighScore
    {
        public string Username;
        public int Score;
    }
}
