using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task5
{
    public class YandexTask5_2
    {
        private readonly Dictionary<string, int> _task2Dictionary = new Dictionary<string, int>();
        private readonly MinSmallDictionary _task2Result = new MinSmallDictionary(5);

        public void ProcessRow(string[] parts)
        {
            //var srcUser = parts[1];
            //var dstUser = parts[4];
            //input_bytes 7
            //output_bytes 8
            var userStartedRequest = parts[1];
            var requestBytes = int.Parse(parts[7]);

            if (string.IsNullOrWhiteSpace(userStartedRequest))
            {
                userStartedRequest = parts[4];
                requestBytes = int.Parse(parts[8]);
            }
            
            int totalBytes = 0;
            if (_task2Dictionary.TryGetValue(userStartedRequest, out totalBytes))
            {
                totalBytes += requestBytes;
            }
            else
            {
                totalBytes = requestBytes;
            }
            _task2Dictionary[userStartedRequest] = totalBytes;
            _task2Result.Add(userStartedRequest, totalBytes);
        }

        public IEnumerable<string> GetResult()
        {
            return _task2Result.Dict.OrderByDescending(p => p.Value).Select(p => $"{p.Value} - {p.Key}");
        }
    }
}
