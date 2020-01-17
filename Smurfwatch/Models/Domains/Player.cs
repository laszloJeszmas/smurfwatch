using Microsoft.AspNetCore.Mvc;
using Smurfwatch.Binders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Smurfwatch.Models
{
    [ModelBinder(BinderType = typeof(PlayerModelBinder))]
    public class Player
    {
        public enum CompetitiveRank
        {
            Bronze,
            Silver,
            Gold,
            Platinum,
            Diamond,
            Master,
            Grandmaster
        }

        public int Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public ISet<Hero> Heroes { get; set; }
        public ISet<Map> Maps { get; set; }
        public ISet<PlayerType> Types { get; set; }
        public string Battletag { get; set; }
        public string Description { get; set; }
        public CompetitiveRank Rank { get; set; }
        [Display(Name = "Main rank")]
        public CompetitiveRank MainRank { get; set; }
        [Display(Name = "Has private profile?")]
        public bool PrivateProfile { get; set; }
    }
}
