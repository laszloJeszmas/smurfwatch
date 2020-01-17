using System.Collections.Generic;
using Dapper;

namespace Smurfwatch.Service.Database
{
    public class Query
    {
        public string QueryText{ get; set; }

        public DynamicParameters Parameters { get; set; }

        public Query(string query, IDictionary<string, string> parameters)
        {
            this.QueryText = query;
            this.Parameters = DictionaryToDynamicParameters(parameters);
        }

        public Query(string query)
        {
            this.QueryText = query;
        }

        private DynamicParameters DictionaryToDynamicParameters(IDictionary<string, string> dictionary)
        {
            var parameters = new DynamicParameters();

            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                parameters.Add(pair.Key, pair.Value);
            }

            return parameters;
        }
    }


}
