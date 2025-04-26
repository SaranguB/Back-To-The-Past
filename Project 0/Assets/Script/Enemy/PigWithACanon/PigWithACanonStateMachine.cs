using StateMachine;
using UnityEngine;
using Wepons.Bomb;

namespace Enemy
{
    public class PigWithACanonStateMachine : GenericStateMachine<PigWithACanonController, EnemyStates>
    {
        public PigWithACanonStateMachine(PigWithACanonController owner, Animator enemyAnimator, Wepons.Bomb.BombPool bombPool) : base(owner)
        {
            CreateState(enemyAnimator, bombPool);
            SetOwner();
        }

        private void CreateState(Animator enemyAnimator, BombPool bombPool)
        {
            AddState(EnemyStates.Idle, new IdleState<PigWithACanonController>(enemyAnimator));
            AddState(EnemyStates.Attack, new AttackState<PigWithACanonController>(enemyAnimator));
        }
    }
}
