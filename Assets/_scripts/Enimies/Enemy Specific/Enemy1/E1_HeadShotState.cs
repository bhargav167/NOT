using Tero.CoreSystem;
using UnityEngine;

namespace Tero
{
    public class E1_HeadShotState : DeadState
    {
        private Enemy1 enemy;
        public E1_HeadShotState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, null)
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
