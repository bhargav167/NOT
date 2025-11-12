using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
using Tero;
public class E1_HiddenState : HiddenState {
        private Enemy1 enemy;
        protected new Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
        private Movement movement;
        protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
        private KnockBackReceiver combat;
        public E1_HiddenState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_HideStateData stateData, Enemy1 enemy1) : base(entity, stateMachine, animBoolName, stateData)
        {
            this.enemy = enemy1;
        }
        public override void DoCheck()
        {
            base.DoCheck();
        }
        public override void Enter()
        {
            base.Enter();
        } 
        public override void Exist(){
            base.Exist();
        }
        public override void LogicUpdate(){
            base.LogicUpdate();
        if (!entity._IsMovedtoOrignalPos)
            HideTimeout();
        else
            stateMachine.ChangeState(enemy.lookingForPlayerState);

        }
        public override void PhysicsUpdate()
        {
                base.PhysicsUpdate();
        } 
}