using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JollyWrapper
{
    //A datatype that can be used to store the result of a query.
    public class QueryData : IEnumerable<Dictionary<string, string>>
    {
        private readonly List<Dictionary<string, string>> _rows = new List<Dictionary<string, string>>();

        public void Add(Dictionary<string, string> href)
        {
            _rows.Add(href);
        }

        public void AddRange(IEnumerable<Dictionary<string, string>> hrefs)
        {
            _rows.AddRange(hrefs);
        }

        public Dictionary<string, string> this[int index]
        {
            get => _rows[index];
        }

        public IEnumerator<Dictionary<string, string>> GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_rows).GetEnumerator();
        }
    }
    public class QueryParms : Dictionary<string, string> { }
}
