using System.Collections;
using System.Collections.Generic;
using Tero.CoreSystem;
using UnityEditorInternal;
using UnityEngine;
 
    public class PlayerStunState : PlayerState
    {
        private readonly Movement movement;

        public PlayerStunState(
            Player player,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            string animBoolName
        ) : base(player, stateMachine, playerData, animBoolName)
        {
            movement = core.getCoreComponents<Movement>();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            movement.SetVelocityX(0f);

            if (Time.time >= startTime + playerData.stunTime)
            {
                stateMachine.ChangeState(Player.IdleState);
            }
        }
    }
