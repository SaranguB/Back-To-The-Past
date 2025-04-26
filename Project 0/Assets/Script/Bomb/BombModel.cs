namespace Wepons.Bomb
{
    public class BombModel
    {
        private BombSO bombSO;

        public int bombDamage;
        public float DamageRadius;
        public float primingTime;

        public BombModel(BombSO bombSO)
        {
            this.bombSO = bombSO;
            this.bombDamage = this.bombSO.bombDamage;
            this.DamageRadius = this.bombSO.DamageRadius;
            this.primingTime = this.bombSO.primingTime;
        }
    }
}
