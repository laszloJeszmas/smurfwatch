using Smurfwatch.Models;
using Smurfwatch.Service.Database;
using System.Collections.Generic;
using System.Linq;

namespace Smurfwatch.Service
{
    public class PlayerTypeService
    {
        private  IRepository<PlayerType> repository;
        private  QueryFactory queryFactory;

        public PlayerTypeService(IRepository<PlayerType> repository, QueryFactory queryFactory)
        {
            this.repository = repository;
            this.queryFactory = queryFactory;
        }

        public IList<PlayerType> GetAllPlayerTypes()
        {
            return repository.GetMultiple(queryFactory.GetAllPlayerTypesQuery());
        }

        public ISet<PlayerType> GetPlayerTypesByPlayerId(int id)
        {
            return repository.GetMultiple(queryFactory.GetPlayerTypesByPlayerIdQuery(id)).ToHashSet();
        }
    }
}
