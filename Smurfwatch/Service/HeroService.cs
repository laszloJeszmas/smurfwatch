using Smurfwatch.Models;
using Smurfwatch.Service.Database;
using System.Collections.Generic;
using System.Linq;

namespace Smurfwatch.Service
{
    public class HeroService
    {
        private IRepository<Hero> repository;
        private QueryFactory queryFactory;

        public HeroService(IRepository<Hero> repository, QueryFactory queryFactory)
        {
            this.repository = repository;
            this.queryFactory = queryFactory;
        }

        public IList<Hero> GetAllHeroes()
        {
            return repository.GetMultiple(queryFactory.GetAllHeroesQuery());
        }

        public ISet<Hero> GetHeroesByPlayerId(int id)
        {
            return repository.GetMultiple(queryFactory.GetHeroesByPlayerIdQuery(id)).ToHashSet();
        }
    }
}
