using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tero
{
    public class E2_DeadState : DeadState
    {
        private Enemy2 enemy;
        public E2_DeadState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, null)
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
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
