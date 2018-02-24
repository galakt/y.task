using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task5
{
    public class YandexTask5_5
    {
        private Dictionary<string, char> CustomLanguageDict { get; } = new Dictionary<string, char>();
        private char _nextCustomLanguageWord = (char)1;

        private readonly char[] last5CharsStorage = new char[5];
        private byte storageSize = 0;

        public MinSmallDictionary Result3Gram = new MinSmallDictionary(5);
        public Dictionary<string, int> Gram3CountDict = new Dictionary<string, int>();

        public MinSmallDictionary Result4Gram = new MinSmallDictionary(5);
        public Dictionary<string, int> Gram4CountDict = new Dictionary<string, int>();
        
        public MinSmallDictionary Result5Gram = new MinSmallDictionary(5);
        public Dictionary<string, int> Gram5CountDict = new Dictionary<string, int>();

        public void ProcessRow(string[] parts)
        {
            //user+src_port+dest_ip+dest_port
            var srcUser = parts[1];
            var srcPort = parts[3];
            var destIp = parts[5];
            var destPort = parts[6];

            var customLanguageWordType = srcUser +","+ srcPort + "," + destIp + "," + destPort;
            char customLanguageWord;
            if (!CustomLanguageDict.TryGetValue(customLanguageWordType, out customLanguageWord))
            {
                customLanguageWord = _nextCustomLanguageWord;
                _nextCustomLanguageWord = ++_nextCustomLanguageWord;
                CustomLanguageDict.Add(customLanguageWordType, customLanguageWord);
            }

            // Full chars storage
            if (storageSize == 5)
            {
                // shift chars
                for (int i = 0; i < 4; i++)
                {
                    last5CharsStorage[i] = last5CharsStorage[i + 1];
                }
                last5CharsStorage[4] = customLanguageWord;

                var gram3 = new string(last5CharsStorage, 2, 3);
                UpdateGramResult(gram3, Gram3CountDict, Result3Gram);

                var gram4 = new string(last5CharsStorage, 1, 4);
                UpdateGramResult(gram4, Gram4CountDict, Result4Gram);

                var gram5 = new string(last5CharsStorage, 0, 5);
                UpdateGramResult(gram5, Gram5CountDict, Result5Gram);

                return;
            }

            // First 5
            if (storageSize == 4)
            {
                last5CharsStorage[storageSize] = customLanguageWord;
                storageSize += 1;

                var gram3 = new string(last5CharsStorage, 2, 3);
                UpdateGramResult(gram3, Gram3CountDict, Result3Gram);

                var gram4 = new string(last5CharsStorage, 1, 4);
                UpdateGramResult(gram4, Gram4CountDict, Result4Gram);

                var gram5 = new string(last5CharsStorage, 0, 5);
                UpdateGramResult(gram5, Gram5CountDict, Result5Gram);

                return;
            }

            // First 4
            if (storageSize == 3)
            {
                last5CharsStorage[storageSize] = customLanguageWord;
                storageSize += 1;

                var gram3 = new string(last5CharsStorage, 1, 3);
                UpdateGramResult(gram3, Gram3CountDict, Result3Gram);

                var gram4 = new string(last5CharsStorage, 0, 4);
                UpdateGramResult(gram4, Gram4CountDict, Result4Gram);

                return;
            }

            // First 3
            if (storageSize == 2)
            {
                last5CharsStorage[storageSize] = customLanguageWord;
                storageSize += 1;

                var gram3 = new string(last5CharsStorage, 0, 3);
                UpdateGramResult(gram3, Gram3CountDict, Result3Gram);

                return;
            }

            last5CharsStorage[storageSize] = customLanguageWord;
            storageSize += 1;
        }

        private void UpdateGramResult(string gram, Dictionary<string, int> countDict, MinSmallDictionary resultDict)
        {
            int count = 0;
            if (countDict.TryGetValue(gram, out count))
            {
                count += 1;
            }
            else
            {
                count = 1;
            }

            countDict[gram] = count;
            resultDict.Add(gram, count);
        }

        public IEnumerable<string> GetResult()
        {
            var sb = new StringBuilder();
            sb.AppendLine("3 gram:");

            foreach (var item in Result3Gram.Dict.OrderByDescending(p=> p.Value))
            {

            }

            return new List<string>(); //_task1Result.Dict.OrderByDescending(p => p.Value).Select(p => $"{p.Key} - {p.Value}");
        }
    }
}
