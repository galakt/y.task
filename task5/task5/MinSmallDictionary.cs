using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task5
{
    public class MinSmallDictionary
    {
        public Dictionary<string, int> Dict { get; }
        private int Capacity { get; }
        public int MinValue { get; private set; }

        public MinSmallDictionary(int capacity)
        {
            Capacity = capacity;
            Dict = new Dictionary<string, int>(capacity);
            MinValue = -1;
        }

        public void Add(string key, int newValue)
        {
            if (Dict.Count < Capacity)
            {
                int oldValue;
                if (Dict.TryGetValue(key, out oldValue))
                {
                    Dict[key] = newValue;
                    NormalizeMin(oldValue);
                }
                else
                {
                    Dict.Add(key, newValue);
                    NormalizeMin(MinValue);
                }

                return;
            }

            if (newValue > MinValue)
            {
                int oldValue;
                if (Dict.TryGetValue(key, out oldValue))
                {
                    Dict[key] = newValue;
                    NormalizeMin(oldValue);
                }
                else
                {
                    var minPair = Dict.First(p => p.Value == MinValue);
                    Dict.Remove(minPair.Key);
                    Dict.Add(key, newValue);

                    NormalizeMin(MinValue);
                }
            }
        }

        private void NormalizeMin(int oldValue)
        {
            if (oldValue != MinValue)
            {
                return;
            }

            MinValue = Dict.Values.Min();
        }
    }
}
