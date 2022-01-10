using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JollyWrapper
{
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

        //Returns the selected cell value.
        public object GetCell(int row, string columnName)
        {
            return _rows[row][columnName];
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
