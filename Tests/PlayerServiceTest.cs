using Functional.Maybe;
using Moq;
using Smurfwatch.Models;
using Smurfwatch.Models.Exceptions;
using Smurfwatch.Service;
using Smurfwatch.Service.Database;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class PlayerServiceTest
    {
        private const string QueryString = "query";
        private const string PlayerId = "id";

        [Fact]
        public void TestGetPlayerByIdWhenPlayerIsFoundThenReturnProperly()
        {
            Query query = buildQuery();
            Maybe<Player> player = new Player().ToMaybe();
            var queryFactory = new Mock<QueryFactory>();
            var repository = new Mock<IRepository<Player>>();

            queryFactory.Setup(q => q.GetPlayerByIdQuery(PlayerId)).Returns(query);
            repository.Setup(r => r.getSingle(query)).Returns(player);
            PlayerService underTest = new PlayerService(repository.Object, queryFactory.Object);

            Player actual = underTest.GetPlayerById(PlayerId);

            Assert.Equal(actual, player.Value);

            queryFactory.Verify(mock => mock.GetPlayerByIdQuery(PlayerId));
            repository.Verify(mock => mock.getSingle(query));
        }

        [Fact]
        public void TestGetPlayerByIdWhenPlayerIsNotFoundThenThrowObjectNotFoundException()
        {
            Query query = buildQuery();
            var queryFactory = new Mock<QueryFactory>();
            var repository = new Mock<IRepository<Player>>();

            queryFactory.Setup(q => q.GetPlayerByIdQuery(PlayerId)).Returns(query);
            repository.Setup(r => r.getSingle(query)).Returns(Maybe<Player>.Nothing);
            PlayerService underTest = new PlayerService(repository.Object, queryFactory.Object);

            Assert.Throws<ObjectNotFoundException>(() => underTest.GetPlayerById(PlayerId));

            queryFactory.Verify(mock => mock.GetPlayerByIdQuery(PlayerId));
            repository.Verify(mock => mock.getSingle(query));
        }

        private Query buildQuery()
        {
            return new Query(QueryString);
        }

    }
}
