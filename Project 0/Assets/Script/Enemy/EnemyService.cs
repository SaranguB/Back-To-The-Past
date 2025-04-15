using UnityEngine;

namespace Enemy
{
    public class EnemyService
    {
        private EnemyController enemy;
        private EnemyViewCollection enemyViewCollection;
        public EnemyService(EnemyViewCollection enemyViewCollection)
        {
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
                        enemy =  new PigController(view);
                        break;
                }
            }
        }

    }
}
