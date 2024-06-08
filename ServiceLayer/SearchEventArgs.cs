using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class SearchEventArgs
    {
        public string Query { get; }

        public SearchEventArgs(string query)
        {
            Query = query;
        }
    }
}
