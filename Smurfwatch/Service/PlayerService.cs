using Functional.Maybe;
using Smurfwatch.Models;
using Smurfwatch.Models.Exceptions;
using Smurfwatch.Service.Database;
using System.Collections.Generic;

namespace Smurfwatch.Service
{
    public class PlayerService
    {
        private readonly IRepository<Player> repository;
        private readonly QueryFactory queryFactory;
        private readonly HeroService heroService;
        private readonly MapService mapService;
        private readonly PlayerTypeService playerTypeService;

        public PlayerService(IRepository<Player> repository, QueryFactory queryFactory, HeroService heroService, MapService mapService, PlayerTypeService playerTypeService)
        {
            this.repository = repository;
            this.queryFactory = queryFactory;
            this.heroService = heroService;
            this.mapService = mapService;
            this.playerTypeService = playerTypeService;
        }

        public IList<Player> GetAllPlayer()
        {
            return repository.GetMultiple(queryFactory.GetAllPlayerQuery());
        }


        public Player GetPlayerById(int id)
        {
            Player player = repository.GetSingle(queryFactory.GetPlayerByIdQuery(id))
                .OrElse(() => new ObjectNotFoundException("Player is not found with the provided id: " + id));

            player.Heroes = heroService.GetHeroesByPlayerId(player.Id);
            player.Maps = mapService.GetMapsByPlayerId(player.Id);
            player.Types = playerTypeService.GetPlayerTypesByPlayerId(player.Id);

            return player;
        }

        public void AddPlayer(Player player)
        {
            string playerId = repository.AddAndGetId<string>(queryFactory.GetAddPlayerQuery(player));

            foreach (Hero hero in player.Heroes)
            {
                repository.Add(queryFactory.GetAddPlayerHeroQuery(playerId, hero.Id.ToString()));
            }

            foreach (Map map in player.Maps)
            {
                repository.Add(queryFactory.GetAddPlayerMapQuery(playerId, map.Id.ToString()));
            }

            foreach (PlayerType playerType in player.Types)
            {
                repository.Add(queryFactory.GetAddPlayerPlayerTypeQuery(playerId, playerType.Id.ToString()));
            }
        }

        public void DeletePlayer(int id)
        {
            repository.Delete(queryFactory.GetDeletePlayerQuery(id));
        }
    }
}
