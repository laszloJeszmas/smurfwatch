using Functional.Maybe;
using Smurfwatch.Models;
using Smurfwatch.Models.Exceptions;
using Smurfwatch.Service.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smurfwatch.Service
{
    public class MapService
    {
        private  IRepository<Map> repository;
        private  QueryFactory queryFactory;

        public MapService(IRepository<Map> repository, QueryFactory queryFactory)
        {
            this.repository = repository;
            this.queryFactory = queryFactory;
        }

        public IList<Map> GetAllMaps()
        {
            return repository.GetMultiple(queryFactory.GetAllMapsQuery());
        }

        public ISet<Map> GetMapsByPlayerId(int id)
        {
            return repository.GetMultiple(queryFactory.GetMapsByPlayerIdQuery(id)).ToHashSet();
        }
    }
}
