using Microsoft.AspNetCore.Mvc.ModelBinding;
using Smurfwatch.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using static Smurfwatch.Models.Player;

namespace Smurfwatch.Binders
{
    public class PlayerModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            IValueProvider valueProvider = bindingContext.ValueProvider;
            Func<string, string> getFirstValue = key => valueProvider.GetValue(key).FirstValue;
            
            Player player = new Player
            {
                Level = Int32.Parse(getFirstValue("level")),
                Name = getFirstValue("name"),
                Battletag = getFirstValue("battletag"),
                Description = getFirstValue("description"),
                Rank = (CompetitiveRank)Enum.Parse(typeof(CompetitiveRank), getFirstValue("rank")),
                MainRank = (CompetitiveRank)Enum.Parse(typeof(CompetitiveRank), getFirstValue("main rank")),
                Heroes = getHeroIds(valueProvider.GetValue("heroes")),
                Maps = getMapIds(valueProvider.GetValue("maps")),
                Types = getPlayerTypeIds(valueProvider.GetValue("playerTypes")),
                PrivateProfile = Boolean.Parse(getFirstValue("privateProfile"))
            };

            bindingContext.Result = ModelBindingResult.Success(player);

            return Task.CompletedTask;
        }

        private ISet<Hero> getHeroIds(ValueProviderResult formResult)
        {
            IEnumerable<Hero> heroIds = formResult.Values
                .Select(id => new Hero { Id = Int32.Parse(id)});
            return new HashSet<Hero>(heroIds);
        }

        private ISet<Map> getMapIds(ValueProviderResult formResult)
        {
            IEnumerable<Map> mapIds = formResult.Values
                .Select(id => new Map { Id = Int32.Parse(id) });
            return new HashSet<Map>(mapIds);
        }

        private ISet<PlayerType> getPlayerTypeIds(ValueProviderResult formResult)
        {
            IEnumerable<PlayerType> playerTypeIds = formResult.Values
                .Select(id => new PlayerType { Id = Int32.Parse(id) });
            return new HashSet<PlayerType>(playerTypeIds);
        }
    } 
}
