using UnityEngine;

namespace Wepons.Bomb
{
    public class BombController
    {
        private BombView bombView;
        private BombModel bombModel;
        public BombController(BombView bombPrefab, BombSO bombSO)
        {
            bombView = Object.Instantiate(bombPrefab);
            bombView.SetController(this);

            bombModel = new BombModel(bombSO);
        }

        public void ConfigureBomb(Transform bombPosition)
        {
            bombView.ConfigurePosition(bombPosition);
        }
    }
}
