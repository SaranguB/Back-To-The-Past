using UnityEngine;

namespace Enemy
{
    public class PigController : EnemyController
    {
        private EnemySO data;

        public PigController(EnemySO data)
        {
            this.data = data;
        }
    }
}
