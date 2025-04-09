using UnityEngine;

namespace Wepons.Bomb
{
    public class BombModel
    {
        private BombSO bombSO;

        public float bombaDamage;

        public BombModel(BombSO bombSO)
        {
            this.bombSO = bombSO;

            bombaDamage = this.bombSO.Damage;
        }

    }
}
