using System.Collections;
using System.Collections.Generic;
using Tero.CoreSystem;
using UnityEditorInternal;
using UnityEngine;

namespace Tero
{
    public class E2_KnockBack : KnockBack
    {
        private Enemy2 _enemy;
        protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
        private KnockBackReceiver combat;
        protected Death Death { get => death ?? core.getCoreComponents(ref death); }
        private Death death;
        public E2_KnockBack(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, null)
        {
            this._enemy = enemy;
        }
        public override void DoCheck()
        {
            base.DoCheck();
        }
        public override void Enter()
        {
            base.Enter();
        }
        public override void Exist()
        {

            base.Exist();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isPlayerIsInMinAgroFrontRange || isPlayerIsInMinAgroBackRange || isPlayerIsInMaxAgroBackRange)
            {
                stateMachine.ChangeState(_enemy.rangedAttactState);
            }
            else
            {
                stateMachine.ChangeState(_enemy.lookForPlayerState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
