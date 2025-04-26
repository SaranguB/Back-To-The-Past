using Wepons.Bomb;

namespace Enemy
{
    public class EnemyService
    {
        private EnemyController enemy;
        private EnemyViewCollection enemyViewCollection;
        private BombPool bombPool;

        public EnemyService(EnemyViewCollection enemyViewCollection, BombSO bombSO, BombView bombPrefab)
        {
            bombPool = new BombPool(bombSO, bombPrefab);
            this.enemyViewCollection = enemyViewCollection;
            CreateEnemies();
        }

        private void CreateEnemies()
        {
            foreach (EnemyView view in enemyViewCollection.enemyView)
            {
                switch (view.enemyData.enemyType)
                {
                    case EnemyType.Pig:
                        enemy = new PigController(view);
                        break;
                    case EnemyType.PigWithACanon:
                        enemy = new PigWithACanonController(view, bombPool);
                        break;
                }
            }
        }
    }
}
