using UnityEngine;

namespace Level
{
    public class LevelService
    {
        private LevelController levelController;

        public LevelService(LevelView levelView)
        {
            levelController = new LevelController(levelView);
        }
    }
}
