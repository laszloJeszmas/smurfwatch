using Smurfwatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smurfwatch.Service.Database
{
    public class QueryFactory
    {
        public virtual Query GetMapsByPlayerIdQuery(int id)
        {
            var parameters = new Dictionary<string, string> { { "id", id.ToString() } };

            string queryText = $"SELECT Id, Name FROM Map " +
                $"JOIN PlayerMap ON PlayerMap.MapId = Map.Id " +
                $"WHERE PlayerId = @Id";

            return CreateQuery(queryText, parameters);
        }

        public virtual Query GetHeroesByPlayerIdQuery(int id)
        {
            var parameters = new Dictionary<string, string> { { "id", id.ToString() } };

            string queryText = $"SELECT Id, Name FROM Hero " +
                $"JOIN PlayerHero ON PlayerHero.HeroId = Hero.Id " +
                $"WHERE PlayerId = @Id";

            return CreateQuery(queryText, parameters);
        }

        public virtual Query GetPlayerTypesByPlayerIdQuery(int id)
        {
            var parameters = new Dictionary<string, string> { { "id", id.ToString() } };

            string queryText = $"SELECT Id, Name FROM PlayerType " +
                $"JOIN PlayerPlayerType ON PlayerPlayerType.TypeId = PlayerType.Id " +
                $"WHERE PlayerId = @Id";

            return CreateQuery(queryText, parameters);
        }

        public virtual Query GetAllPlayerQuery()
        {
            return CreateQuery("SELECT * FROM Player");
        }

        public virtual Query GetPlayerByIdQuery(int id)
        {
            var parameters = new Dictionary<string, string> { { "id", id.ToString() } };

            return CreateQuery("SELECT * FROM Player WHERE Id=@Id", parameters);
        }

        public virtual Query GetAllHeroesQuery()
        {
            return CreateQuery("SELECT * FROM Hero");
        }

        public virtual Query GetAllMapsQuery()
        {
            return CreateQuery("SELECT * FROM Map");
        }

        public virtual Query GetAllPlayerTypesQuery()
        {
            return CreateQuery("SELECT * FROM PlayerType");
        }

        //public virtual IList<Query> GetAddPlayerQueries(Player player)
        //{
        //    IList<Query> result = new List<Query>();

        //    var createPlayerQueryText = "INSERT INTO Player (Name, Level, Battletag, Description, Rank, MainRank, PrivateProfile)" +
        //        "VALUES (@Name, @Level, @Battletag, @Description, @Rank, @MainRank, @PrivateProfile); SELECT (SCOPE_IDENTITY() AS INT);";
        //    var createPlayerQueryParameters = new Dictionary<string, string>
        //    {
        //        { "Name", player.Name },
        //        { "Level", player.Level.ToString() },
        //        { "Battletag", player.Battletag },
        //        { "Description", player.Description },
        //        { "Rank", player.Rank.ToString() },
        //        { "MainRank", player.MainRank.ToString() },
        //        { "PrivateProfile", player.PrivateProfile.ToString() },
        //    };

        //    result.Add(CreateQuery(createPlayerQueryText, createPlayerQueryParameters));
        //    result.Add(CreateQuery("BEGIN"));
        //    result.Add(CreateQuery("DECLARE @playerid INT"));
        //    result.Add(CreateQuery("SELECT @playerid = SCOPE_IDENTITY()"));

        //    foreach (Hero hero in player.Heroes)
        //    {
        //        var parameters = new Dictionary<string, string> { { "HeroId", hero.Id.ToString() } };
        //        result.Add(CreateQuery("INSERT INTO PlayerHero (PlayerId, HeroId) VALUES (playerid, @HeroId)"));
        //    }

        //    foreach (Map map in player.Maps)
        //    {
        //        var parameters = new Dictionary<string, string> { { "MapId", map.Id.ToString() } };
        //        result.Add(CreateQuery("INSERT INTO PlayerMap (PlayerId, MapId) VALUES (playerid, @MapId)"));
        //    }

        //    foreach (PlayerType type in player.Types)
        //    {
        //        var parameters = new Dictionary<string, string> { { "PlayerTypeId", type.Id.ToString() } };
        //        result.Add(CreateQuery("INSERT INTO PlayerPlayerType (PlayerId, TypeId) VALUES (playerid, @PlayerTypeId)"));
        //    }

        //    return result;
        //}

        public virtual Query GetAddPlayerQuery(Player player)
        {
            var createPlayerQueryText = "INSERT INTO Player (Name, Level, Battletag, Description, Rank, MainRank, PrivateProfile)" +
                "VALUES (@Name, @Level, @Battletag, @Description, @Rank, @MainRank, @PrivateProfile); SELECT CAST(SCOPE_IDENTITY() AS INT);";
            var createPlayerQueryParameters = new Dictionary<string, string>
            {
                { "Name", player.Name },
                { "Level", player.Level.ToString() },
                { "Battletag", player.Battletag },
                { "Description", player.Description },
                { "Rank", player.Rank.ToString() },
                { "MainRank", player.MainRank.ToString() },
                { "PrivateProfile", player.PrivateProfile.ToString() },
            };

            return CreateQuery(createPlayerQueryText, createPlayerQueryParameters);
        }

        public virtual Query GetAddPlayerPlayerTypeQuery(string playerId, string playerTypeId)
        {
            var parameters = new Dictionary<string, string> { { "PlayerTypeId", playerTypeId }, { "PlayerId", playerId } };
            return CreateQuery("INSERT INTO PlayerPlayerType (PlayerId, TypeId) VALUES (@PlayerId, @PlayerTypeId)", parameters);
        }

        public virtual Query GetAddPlayerHeroQuery(string playerId, string heroId)
        {
            var parameters = new Dictionary<string, string> { { "HeroId", heroId }, { "PlayerId", playerId } };
            return CreateQuery("INSERT INTO PlayerHero (PlayerId, HeroId) VALUES (@PlayerId, @HeroId)", parameters);
        }

        public virtual Query GetAddPlayerMapQuery(string playerId, string mapId)
        {
            var parameters = new Dictionary<string, string> { { "MapId", mapId}, { "PlayerId", playerId } };
            return CreateQuery("INSERT INTO PlayerMap (PlayerId, MapId) VALUES (@PlayerId, @MapId)", parameters);
        }

        public virtual Query GetDeletePlayerQuery(int playerId)
        {
            var parameters = new Dictionary<string, string> { { "Id", playerId.ToString() } };
            return CreateQuery(
                    "DELETE FROM PlayerHero WHERE PlayerId = @Id;" +
                    "DELETE FROM PlayerMap WHERE PlayerId = @Id;" +
                    "DELETE FROM PlayerPlayerType WHERE PlayerId = @Id;" +
                    "DELETE FROM Player WHERE Id = @Id;",
                    parameters
                );
        }


        private Query CreateQuery(string query)
        {
            return new Query(query);
        }

        private Query CreateQuery(string query, IDictionary<string, string> parameters)
        {
            return new Query(query, parameters);
        }
    }
}
