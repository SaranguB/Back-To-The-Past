using ObjectPool;
using UnityEngine;

namespace Wepons.Bomb
{
    public class BombPool : GenericObjectPool<BombController>
    {
        private BombView bombPrefab;
        private BombSO bombSO;

        public BombPool(BombSO bombSO, BombView bombPrefab)
        {
            this.bombSO = bombSO;
            this.bombPrefab = bombPrefab;
        }

        public BombController GetBomb()
             => GetItem<BombController>();

        protected override BombController CreateItem<T>()
                => new BombController(bombPrefab, bombSO);
    }
}
