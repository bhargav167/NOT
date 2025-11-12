using Tero;
using UnityEngine;

  public class Enemy2 : Entity {
      public E2_MoveState moveState { get; private set; }
      public E2_IdleState idleState { get; private set; }
      public E2_PlayerDetectedState playerDetectedState { get; private set; }
      public E2_MeeleAttactState meeleAttactState { get; private set; }
      public E2_LookForPlayerState lookForPlayerState { get; private set; }
      public E2_JumpState jumpState { get; private set; }
      public E2_DodgeState dodgeState { get; private set; }
      public E2_KnockBack knockState { get; private set; }
      public E2_GranadeKnockBack granadeknockState { get; private set; }
      public E2_HideState hideState { get; private set; }
      public E2_HiddenState hiddenState { get; private set; }
      public E2_DeadState deadState { get; private set; }
      public E2_HeadShotState headshotState { get; private set; }
      public E2_RangedAttactState rangedAttactState { get; private set; }

      [SerializeField]
      private D_MoveState moveStateData;
      [SerializeField]
      private D_IdleState idleStateData;
      [SerializeField]
      private D_PlayerDetctData playerDetctData;
      [SerializeField]
      private D_MeleeAttact meeleAttactData;
      [SerializeField]
      private D_LookForPlayer lookForPlayerStateData;
      [SerializeField]
      private D_JumpState jumpData;
      [SerializeField]
      public D_DodgeState dodgeStateData;
      [SerializeField]
      public D_RangedAttactState rangedAttactStateData;
      [SerializeField]
      private Transform meeleAttactposition;
      [SerializeField]
      private Transform rangedAttackPosition;
      public override void Awake () {
          base.Awake ();
          moveState = new E2_MoveState (this, stateMachine, "move", moveStateData, this);
          idleState = new E2_IdleState (this, stateMachine, "Idle", idleStateData, this);
          playerDetectedState = new E2_PlayerDetectedState (this, stateMachine, "playerDetected", playerDetctData, this);
          meeleAttactState = new E2_MeeleAttactState (this, stateMachine, "meeleAttact", meeleAttactposition, meeleAttactData, this);
          jumpState = new E2_JumpState (this, stateMachine, "Jumping", jumpData, this);
          lookForPlayerState = new E2_LookForPlayerState (this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
          dodgeState = new E2_DodgeState (this, stateMachine, "dodge", dodgeStateData, this);
          knockState = new E2_KnockBack(this, stateMachine, "hurt", null, this);
          granadeknockState = new E2_GranadeKnockBack(this, stateMachine, "knockByGrenade", null, this);
          rangedAttactState = new E2_RangedAttactState (this, stateMachine, "rangedAttack",rangedAttackPosition, rangedAttactStateData, this);
          hideState = new E2_HideState(this, stateMachine, "moveTohide", hideStateData, this);
          hiddenState = new E2_HiddenState(this, stateMachine, "hidden", hideStateData, this);
          deadState = new E2_DeadState(this, stateMachine, "dead", null, this);
          headshotState = new E2_HeadShotState(this, stateMachine, "HeadShot", null, this);

    }
      private void Start(){
       stateMachine.Initilize (moveState);   
      }
      public override void OnDrawGizmos () {
          base.OnDrawGizmos ();
          Gizmos.DrawWireSphere (meeleAttactposition.position, meeleAttactData.attactRadius);
      }
  }