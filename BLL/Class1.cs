using FightingGame.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame.BLL
{
    using FightingGame.Domain;

    public class CharacterService
    {
        public Character Create(int speed, int varAttr)
        {
            return new Character(speed, varAttr);
        }

        public void Hit(Character c, HitType type)
        {
            c.ReceiveHit(type);
        }

        public void FullRestore(Character c)
        {
            c.RestoreToFull();
        }
    }
}
