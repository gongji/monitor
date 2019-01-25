/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestHTTP.Extensions
{
    /// <summary>
    /// Base class for specialized parsers
    /// </summary>
    public class KeyValuePairList
    {
        public List<HeaderValue> Values { get; protected set; }

        public bool TryGet(string value, out HeaderValue @param)
        {
            @param = null;
            for (int i = 0; i < Values.Count; ++i)
                if (string.CompareOrdinal(Values[i].Key, value) == 0)
                {
                    @param = Values[i];
                    return true;
                }
            return false;
        }
    }
}
