using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporerSteve.Services
{
    public class Traveller2eDiceRoller
    {
        private Random _random;

        public Traveller2eDiceRoller()
        {
            _random = new Random();
        }

        public int RollD6()
        {
            return new Random().Next(1, 7);
        }

        public int Roll2D6()
        {
            return RollD6() + RollD6();
        }

        public int RollD3()
        {
            return (int)Math.Ceiling(RollD6() / 2.0);
        }

        public int RollD66()
        {
            return (RollD6() * 10) + RollD6();
        }
    }
}
