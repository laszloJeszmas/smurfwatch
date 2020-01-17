using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Functional.Maybe;

namespace Smurfwatch.Service.Database
{
    public class Repository<T> : IRepository<T>
    {
        private readonly ConnectorFactory connectorFactory;

        public Repository(ConnectorFactory connector)
        {
            this.connectorFactory = connector;
        }

        public Maybe<T> GetSingle(Query query)
        { 
            using (IDbConnection connection = connectorFactory.GetConnection())
            {
                return connection.QuerySingleOrDefault<T>(query.QueryText, query.Parameters).ToMaybe();
                }
        }

        public void Add(IList<Query> queries)
        {
            using (IDbConnection connection = connectorFactory.GetConnection())
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    foreach (Query query in queries)
                    {
                        connection.Execute(query.QueryText, query.Parameters, transaction);
                    }

                    transaction.Commit();
                }
            }
        }

        public B AddAndGetId<B>(Query query)
        {
            using (IDbConnection connection = connectorFactory.GetConnection())
            {
                return connection.Query<B>(query.QueryText, query.Parameters).Single();
            }
        }


        public void Add(Query query)
        {
            using (IDbConnection connection = connectorFactory.GetConnection())
            {
                connection.Execute(query.QueryText, query.Parameters);
            }
        }


        public IList<T> GetMultiple(Query query)
        {
            using (IDbConnection connection = connectorFactory.GetConnection())
            {
                return connection.Query<T>(query.QueryText, query.Parameters).AsList();
            }
        }

        public void Delete(Query query)
        {
            using (IDbConnection connection = connectorFactory.GetConnection())
            {
                connection.Open();
                connection.Execute(query.QueryText, query.Parameters);
            }
        }

        private DynamicParameters DictionaryToDynamicParameters(IDictionary<string, object> dictionary)
        {
            var parameters = new DynamicParameters();
            
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                parameters.Add(pair.Key, pair.Value);
            }

            return parameters;
        }
    }
}
