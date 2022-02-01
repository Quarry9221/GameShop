using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Domain.Entities
{
    public class CartLine
    {
        public Game Game { get; set; }
        public int Count { get; set; }
    }
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void Add(Game game_, int count_)
        {
            CartLine line = lineCollection
                .Where(g => g.Game.Id == game_.Id)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Game = game_,
                    Count = count_
                });
            }
            else
            {
                line.Count += count_;
            }
        }
        public void Remove(Game game)
        {
            lineCollection.RemoveAll(l => l.Game.Id == game.Id);
        }
        public decimal CalculateValue()
        {
            return lineCollection.Sum(e => e.Game.Price * e.Count);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

    }
}
