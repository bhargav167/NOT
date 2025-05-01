using Tero;
using UnityEngine;
public class Enemy1 : Entity {
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_LookForPlayerState lookingForPlayerState { get; private set; }
    public E1_MeleeAttactState meleeAttactState { get; private set; }
    public E1_JumpState jumpState { get; private set; }
    public E1_KnockBack knockState { get; private set; }
    public E1_HeadKnockBack headknockState { get; private set; }
    public E1_LegKnockBack legknockState { get; private set; }
    public E1_GranadeKnockBack granadeknockState { get; private set; }
    public E1_StunState stunState { get; private set; }
    public E1_DeadState deadState { get; private set; }
    public E1_HeadShotState headshotState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetctData playerDetectStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttact meleeAttactStateData;
    [SerializeField]
    private D_JumpState jumpStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private Transform meleeAttactPosition;
    [SerializeField]
    private D_GroundState groundStateData;
    public override void Awake(){
        base.Awake();
        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetect", playerDetectStateData, this);
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookingForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookingforplayer", lookForPlayerStateData, this);
        meleeAttactState = new E1_MeleeAttactState(this, stateMachine, "meleeAttact", meleeAttactPosition, meleeAttactStateData, this);
        jumpState = new E1_JumpState(this, stateMachine, "Jump", jumpStateData, this);
        knockState = new E1_KnockBack(this, stateMachine, "hurt", null, this);
        headknockState = new E1_HeadKnockBack(this, stateMachine, "headhurt", null, this);
        legknockState = new E1_LegKnockBack(this, stateMachine, "LegsShot", null, this);
        granadeknockState = new E1_GranadeKnockBack(this, stateMachine, "knockByGrenade", null, this);
        stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E1_DeadState(this, stateMachine, "dead", null, this);
        headshotState = new E1_HeadShotState(this, stateMachine, "HeadShot", null, this);
    }

    private void Start(){
        stateMachine.Initilize (moveState);
    }
    public override void OnDrawGizmos () {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttactPosition.position,meleeAttactStateData.attactRadius);
         
    }
}