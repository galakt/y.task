using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task5
{
    public class YandexTask5_1
    {
        private readonly Dictionary<string, int> _task1Dictionary = new Dictionary<string, int>();
        private readonly MinSmallDictionary _task1Result = new MinSmallDictionary(5);

        public void ProcessRow(string[] parts)
        {
            var srcUser = parts[1];
            var dstUser = parts[4];
            var userStartedRequest = srcUser;

            if (string.IsNullOrWhiteSpace(srcUser))
            {
                userStartedRequest = dstUser;
            }
            
            int requestCount = 0;
            if (_task1Dictionary.TryGetValue(userStartedRequest, out requestCount))
            {
                requestCount += 1;
            }
            else
            {
                requestCount = 1;
            }
            _task1Dictionary[userStartedRequest] = requestCount;
            _task1Result.Add(userStartedRequest, requestCount);
        }

        public IEnumerable<string> GetResult()
        {
            return _task1Result.Dict.OrderByDescending(p => p.Value).Select(p => $"{p.Value} - {p.Key}");
        }
    }
}
