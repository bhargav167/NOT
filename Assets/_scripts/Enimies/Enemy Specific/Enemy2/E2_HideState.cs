using Tero;
using Tero.CoreSystem;
using UnityEngine;

    public class E2_HideState:HideState
    {
        private Enemy2 enemy;
        protected new Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
        private Movement movement;
        protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
        private KnockBackReceiver combat;
        public E2_HideState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_HideStateData stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            if (isHidden)
            {
                stateMachine.ChangeState(enemy.hiddenState);
                isHidden = false;
            }
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }