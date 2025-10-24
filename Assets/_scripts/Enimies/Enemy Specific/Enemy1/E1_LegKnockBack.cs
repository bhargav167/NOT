using System.Collections;
using System.Collections.Generic;
using Tero.CoreSystem;
using UnityEditorInternal;
using UnityEngine;

namespace Tero
{
    public class E1_LegKnockBack : KnockBack
    {

        private Enemy1 enemy;
        protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
        private KnockBackReceiver combat;
        protected Death Death { get => death ?? core.getCoreComponents(ref death); }
        private Death death;
        public E1_LegKnockBack(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, null)
        {
            this.enemy = enemy;
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
            if (isPlayerIsInCloseRangeAction)
                stateMachine.ChangeState(enemy.meleeAttactState);

            if (ishiding)
                stateMachine.ChangeState(enemy.hideState);

            else stateMachine.ChangeState(enemy.stunState);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
