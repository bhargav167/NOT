using System.Collections;
using System.Collections.Generic;
using Tero.CoreSystem;
using UnityEngine;

    public class E1_HeadKnockBack : KnockBack
    {
        private Enemy1 enemy;
        protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
        private KnockBackReceiver combat;
        protected Death Death { get => death ?? core.getCoreComponents(ref death); }
        private Death death;
        public E1_HeadKnockBack(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, null)
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
            stateMachine.ChangeState(enemy.deadState);
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