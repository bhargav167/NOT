using System.Collections;
using System.Collections.Generic;
using Tero.CoreSystem;
using UnityEngine;

namespace Tero
{
    public class E1_GranadeKnockBack : KnockBack
    {
        private Enemy1 _enemy;
        protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
        private KnockBackReceiver combat;
        protected Death Death { get => death ?? core.getCoreComponents(ref death); }
        private Death death;
        public E1_GranadeKnockBack(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, null)
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
            if (Combat.isKnockBackByGranadeActive == false)
            {
                stateMachine.ChangeState(_enemy.lookingForPlayerState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
