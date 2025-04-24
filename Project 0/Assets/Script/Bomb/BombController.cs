
using Main;
using Player;
using System.Threading.Tasks;
using UnityEngine;

namespace Wepons.Bomb
{
    public class BombController
    {
        private BombView bombView;
        private BombModel bombModel;
        private BombStateMachine bombStateMachine;
        public BombController(BombView bombPrefab, BombSO bombSO)
        {
            SetViewAndModel(bombPrefab, bombSO);
            CreateStateMachine();
        }

        private void SetViewAndModel(BombView bombPrefab, BombSO bombSO)
        {
            bombView = Object.Instantiate(bombPrefab);
            bombView.SetController(this);
            bombModel = new BombModel(bombSO);
        }

        private void CreateStateMachine()
        {
            bombStateMachine = new BombStateMachine(this);
        }

        public void ConfigureBomb(Transform bombPosition)
        {
            SetBombState(BombState.Primed);
            bombView.gameObject.SetActive(true);
            SetDamageArea();
            bombView.ConfigurePosition(bombPosition);
        }

        private void SetDamageArea()
        {
            bombView.damageArea.SetDamageValues(bombModel.DamageRadius, bombModel.bombDamage);
            ChangeDamageAreaColliderState(false);
        }

        public void ChangeDamageAreaColliderState(bool value)
        {
            bombView.damageArea.ChangeColliderState(value);
        }

        public async void StartBombTimer()
        {
            await Task.Delay((int)(bombModel.primingTime * 1000));

            SetBombState(BombState.Exploding);
        }


        public void SetAnimatorBool(string parameterName, bool boolValue)
        {
            if (bombView.bombAnimator != null)
                bombView.bombAnimator.SetBool(parameterName, boolValue);
        }

        public void SetBombState(BombState state)
        {
            bombStateMachine.ChangeState(state);
        }

        public void DisableBomb()
        {
            bombView.gameObject.SetActive(false);
            GameManager.Instance.playerService.ReturneBombToPool(this);
        }

        public void LaunchBomb(Vector2 throwDirection, float bombThrowForceX, float bombThrowForceY)
        {
            Vector2 throwForce = new Vector2(throwDirection.x * bombThrowForceX, bombThrowForceY);
            bombView.bombRB.AddForce(throwForce, ForceMode2D.Impulse);
        }

        public void PlayBombExposionSound()
        {
            GameManager.Instance.soundService.PlaySoundEffects(Audio.SoundType.ExplosionSound);
        }
    }
}
