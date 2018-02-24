using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task5
{
    public class YandexTask5_5
    {
        private StringBuilder sb = new StringBuilder();
        private const char WordTypeDelim = '+';
        private const char WordsDelim = ',';

        private readonly Dictionary<string, int> _customLanguageDict = new Dictionary<string, int>();
        private readonly Dictionary<int, string> _customLanguageDictReversed = new Dictionary<int, string>();
        private int _nextCustomLanguageWord = 1;

        private readonly int[] _last5CharsStorage = new int[5];
        private byte _storageSize = 0;

        public readonly MinSmallDictionary Result3Gram = new MinSmallDictionary(5);
        public readonly Dictionary<string, int> Gram3CountDict = new Dictionary<string, int>();

        public readonly MinSmallDictionary Result4Gram = new MinSmallDictionary(5);
        public readonly Dictionary<string, int> Gram4CountDict = new Dictionary<string, int>();
        
        public readonly MinSmallDictionary Result5Gram = new MinSmallDictionary(5);
        public readonly Dictionary<string, int> Gram5CountDict = new Dictionary<string, int>();

        public void ProcessRow(string[] parts)
        {
            //user+src_port+dest_ip+dest_port
            var srcUser = parts[1];
            var srcPort = parts[3];
            var destIp = parts[5];
            var destPort = parts[6];

            sb.Clear();
            sb.Append(srcUser);
            sb.Append(WordTypeDelim);
            sb.Append(srcPort);
            sb.Append(WordTypeDelim);
            sb.Append(destIp);
            sb.Append(WordTypeDelim);
            sb.Append(destPort);
            var customLanguageWordType = sb.ToString(); //srcUser +"+"+ srcPort + "+" + destIp + "+" + destPort;
            int customLanguageWord;
            if (!_customLanguageDict.TryGetValue(customLanguageWordType, out customLanguageWord))
            {
                customLanguageWord = _nextCustomLanguageWord;
                _nextCustomLanguageWord = ++_nextCustomLanguageWord;

                _customLanguageDict.Add(customLanguageWordType, customLanguageWord);
                _customLanguageDictReversed.Add(customLanguageWord, customLanguageWordType);
            }

            // Process words storage
            if (_storageSize == 5)
            {
                // shift words
                for (int i = 0; i < 4; i++)
                {
                    _last5CharsStorage[i] = _last5CharsStorage[i + 1];
                }
                _last5CharsStorage[4] = customLanguageWord;

                sb.Clear();
                sb.Append(_last5CharsStorage[2]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[3]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[4]);
                var gram3 = sb.ToString(); //_last5CharsStorage[2].ToString() + "," + _last5CharsStorage[3].ToString() + "," + _last5CharsStorage[4].ToString();
                UpdateGramResult(gram3, Gram3CountDict, Result3Gram);

                sb.Clear();
                sb.Append(_last5CharsStorage[1]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[2]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[3]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[4]);
                var gram4 = sb.ToString(); //_last5CharsStorage[1].ToString() + "," + _last5CharsStorage[2].ToString() + "," + _last5CharsStorage[3].ToString() + "," + _last5CharsStorage[4];
                UpdateGramResult(gram4, Gram4CountDict, Result4Gram);

                sb.Clear();
                sb.Append(_last5CharsStorage[0]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[1]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[2]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[3]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[4]);
                var gram5 = sb.ToString(); //_last5CharsStorage[0].ToString() + "," + _last5CharsStorage[1].ToString() + "," + _last5CharsStorage[2].ToString() + "," + _last5CharsStorage[3] + "," + _last5CharsStorage[4];
                UpdateGramResult(gram5, Gram5CountDict, Result5Gram);

                return;
            }

            // First 5 special case
            if (_storageSize == 4)
            {
                _last5CharsStorage[_storageSize] = customLanguageWord;
                _storageSize += 1;

                sb.Clear();
                sb.Append(_last5CharsStorage[2]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[3]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[4]);
                var gram3 = sb.ToString(); //_last5CharsStorage[2].ToString() + "," + _last5CharsStorage[3].ToString() + "," + _last5CharsStorage[4].ToString();
                UpdateGramResult(gram3, Gram3CountDict, Result3Gram);

                sb.Clear();
                sb.Append(_last5CharsStorage[1]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[2]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[3]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[4]);
                var gram4 = sb.ToString(); //_last5CharsStorage[1].ToString() + "," + _last5CharsStorage[2].ToString() + "," + _last5CharsStorage[3].ToString() + "," + _last5CharsStorage[4].ToString();
                UpdateGramResult(gram4, Gram4CountDict, Result4Gram);

                sb.Clear();
                sb.Append(_last5CharsStorage[0]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[1]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[2]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[3]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[4]);
                var gram5 = sb.ToString(); //_last5CharsStorage[0].ToString() + "," + _last5CharsStorage[1].ToString() + "," + _last5CharsStorage[2].ToString() + "," + _last5CharsStorage[3].ToString() + "," + _last5CharsStorage[4].ToString();
                UpdateGramResult(gram5, Gram5CountDict, Result5Gram);

                return;
            }

            // First 4 special case
            if (_storageSize == 3)
            {
                _last5CharsStorage[_storageSize] = customLanguageWord;
                _storageSize += 1;

                sb.Clear();
                sb.Append(_last5CharsStorage[1]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[2]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[3]);
                var gram3 = sb.ToString(); //_last5CharsStorage[1].ToString() + "," + _last5CharsStorage[2].ToString() + "," + _last5CharsStorage[3].ToString();
                UpdateGramResult(gram3, Gram3CountDict, Result3Gram);

                sb.Clear();
                sb.Append(_last5CharsStorage[0]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[1]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[2]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[3]);
                var gram4 = sb.ToString(); //_last5CharsStorage[0].ToString() + "," + _last5CharsStorage[1].ToString() + "," + _last5CharsStorage[2].ToString() + "," + _last5CharsStorage[3].ToString();
                UpdateGramResult(gram4, Gram4CountDict, Result4Gram);

                return;
            }

            // First 3 special case
            if (_storageSize == 2)
            {
                _last5CharsStorage[_storageSize] = customLanguageWord;
                _storageSize += 1;

                sb.Clear();
                sb.Append(_last5CharsStorage[0]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[1]);
                sb.Append(WordsDelim);
                sb.Append(_last5CharsStorage[2]);
                var gram3 = sb.ToString(); //_last5CharsStorage[0].ToString() + "," + _last5CharsStorage[1].ToString() + "," + _last5CharsStorage[2].ToString();
                UpdateGramResult(gram3, Gram3CountDict, Result3Gram);

                return;
            }

            // Less 3 special case
            _last5CharsStorage[_storageSize] = customLanguageWord;
            _storageSize += 1;
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

        public string GetResult()
        {
            sb.Clear();
            sb.AppendLine("3 gram:");
            foreach (var item in Result3Gram.Dict.OrderByDescending(p=> p.Value))
            {
                sb.AppendLine(item.Value + " - " + UnpackWord(item.Key));
            }

            sb.AppendLine("4 gram:");
            foreach (var item in Result4Gram.Dict.OrderByDescending(p => p.Value))
            {
                sb.AppendLine(item.Value + " - " + UnpackWord(item.Key));
            }

            sb.AppendLine("5 gram:");
            foreach (var item in Result5Gram.Dict.OrderByDescending(p => p.Value))
            {
                sb.AppendLine(item.Value + " - " + UnpackWord(item.Key));
            }

            return sb.ToString();
        }

        private string UnpackWord(string word)
        {
            var numbers = word.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" , ", numbers.Select(p => _customLanguageDictReversed[int.Parse(p)]));
        }
    }
}
