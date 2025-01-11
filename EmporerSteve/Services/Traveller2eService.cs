using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporerSteve.Services
{
    public class Traveller2eService
    {
        private Traveller2eDiceRoller diceRoller;

        public Traveller2eService()
        {
            diceRoller = new();
        }

        public int GetCharacteristicModifier(int characteristic)
        {
            return characteristic switch
            {
                < 1 => -3,
                >= 1 and < 3 => -2,
                >= 3 and < 6 => -1,
                >= 6 and < 9 => 0,
                >= 9 and < 12 => 1,
                >= 12 and < 15 => 2,
                >= 15 => 3
            };
        }

        public int[] GetValidStartingCharacteristics(int minimumModiferSum)
        {
            int[] characteristics;
            do
            {
                characteristics = RollStartingCharacteristics();
            } while (!IsValidStartingCharacteristics(characteristics, minimumModiferSum));

            return characteristics;
        }

        public int[] RollStartingCharacteristics()
        {
            int[] characteristics = new int[6];
            for (int i = 0; i < 6; i++)
            {
                characteristics[i] = diceRoller.Roll2D6();
            }

            return characteristics;
        }

        public bool IsValidStartingCharacteristics(int[] characteristics, int minimumModifierSum)
        {
            int[] modifiers = characteristics.Select(GetCharacteristicModifier).ToArray();

            return modifiers.Sum() >= minimumModifierSum;
        }
    }
}
