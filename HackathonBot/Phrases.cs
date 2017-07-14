using System.Collections.Generic;

namespace HackathonBot
{
    public static class Phrases
    {
        private static readonly Dictionary<string, string> Responses;

        static Phrases()
        {
            //A list of basic phrases and responses that end the conversation
            //to be used to return giffys
            Responses = new Dictionary<string, string>
            {
                {"thanks", "you're welcome"},
                {"thank you", "you're welcome"},
                {"bye", "bye"},
                {"see you", "bye"},
                {"shit", "suprise"},
                {"i hate you", "cry"},
                {"awesome", "awesome"},
                {":(", "happy"},
                {"ok", "you're welcome"},
                {"cheers", "cheers"},
                {"stuff you", "murder"},
                {"bye bye", "bye" },
                {"omg", "omg" },
                {"you suck", "I hate you" },
                {"haha", "haha" }
            };
        }

        public static string GetPhraseResponse(string input)
        {
            return Responses[input];
        }

        public static bool ContainsKey(string key)
        {
            return Responses.ContainsKey(key);
        }
    }
}