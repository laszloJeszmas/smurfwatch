using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smurfwatch.Models;
using Smurfwatch.Service;
using static Smurfwatch.Models.Player;

namespace Smurfwatch.Controllers
{
    public class PlayerController : Controller
    {
        private readonly PlayerService playerService;
        private readonly HeroService heroService;
        private readonly MapService mapService;
        private readonly PlayerTypeService playerTypeService;

        public PlayerController(PlayerService playerService, HeroService heroService, MapService mapService, PlayerTypeService playerTypeService)
        {
            this.playerService = playerService;
            this.heroService = heroService;
            this.mapService = mapService;
            this.playerTypeService = playerTypeService;
        }

        // GET: Player
        public ActionResult Index()
        {
            IList<Player> players = playerService.GetAllPlayer();
            return View(players);
        }

        // GET: Player/Details/5
        public ActionResult Details(int id)
        {
            Player player = playerService.GetPlayerById(id);

            return View(player);
        }

        // GET: Player/Create
        public ActionResult Add()
        {
            IList<Hero> heroes = heroService.GetAllHeroes();
            IList<Map> maps = mapService.GetAllMaps();
            IList<PlayerType> playerTypes = playerTypeService.GetAllPlayerTypes();
            IList<string> ranks = Enum.GetNames(typeof(CompetitiveRank));

            ViewBag.Heroes = new MultiSelectList(heroes, "Id", "Name");
            ViewBag.Maps = new MultiSelectList(maps, "Id", "Name");
            ViewBag.PlayerTypes= new MultiSelectList(playerTypes, "Id", "Name");
            ViewBag.Ranks = ranks;

            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Player player)
        {
            playerService.AddPlayer(player);

            return RedirectToAction(nameof(Index));
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0) throw new SystemException("An ID must be defined.");

            Player player = playerService.GetPlayerById(id);

            IList<Hero> heroes = heroService.GetAllHeroes();
            IList<Map> maps = mapService.GetAllMaps();
            IList<PlayerType> playerTypes = playerTypeService.GetAllPlayerTypes();
            IList<string> ranks = Enum.GetNames(typeof(CompetitiveRank));

            ViewBag.Heroes = new MultiSelectList(heroes, "Id", "Name", player.Heroes.Select(hero => hero.Name));
            ViewBag.Maps = new MultiSelectList(maps, "Id", "Name");
            ViewBag.PlayerTypes = new MultiSelectList(playerTypes, "Id", "Name");
            ViewBag.Ranks = ranks;

            return View(player);
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int id)
        {
            Player player = playerService.GetPlayerById(id);

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
                playerService.DeletePlayer(id);
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(IFormCollection collection)
        {
            return View();
        }
    }
}