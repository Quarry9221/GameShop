using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;

namespace GameShop.Domain.Context
{
    public class EFGameRepository : IGameRepository
    {
        EFGameShopContext context = new EFGameShopContext();
        public IEnumerable<Game> Games
        {
            get { return context.Games; }
        }
        public void SaveGame(Game game)
        {
            if(game.Id == 0)
            {
                context.Games.Add(game);
            }
            else
            {
                Game dbEntry = context.Games.Find(game.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = game.Name;
                    dbEntry.Genre = game.Genre;
                    dbEntry.Date = dbEntry.Date;
                    dbEntry.Developer = game.Developer;
                    dbEntry.Price = game.Price;
                    dbEntry.ImageData = game.ImageData;
                    dbEntry.ImageMimeType = game.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public Game DeleteGame(int Id)
        {
            Game dbEntry = context.Games.Find(Id);
            if (dbEntry != null)
            {
                context.Games.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
