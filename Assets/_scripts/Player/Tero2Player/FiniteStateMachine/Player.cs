using UnityEngine;
using Tero.Weapons;
using Tero.Weapons.Components;
using Tero.CoreSystem;
using UnityEditorInternal;

public class Player : MonoBehaviour
{
    #region StateVariable
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PalyerLandState LandState { get; private set; }
    public PlayerWallSlidState WallSlideState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerAttackState AK47AttackState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondryAttackState { get; private set; }
    public PlayerAttackState AttackMoveState { get; private set; }
    public PlayerAttackState rangedAttactState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerKnockBackState KnockBackState { get; private set; }
    public PlayerStunState PlayerStunState { get; private set; }
    public PlayerKnockBackByGrenadeState PlayerKnockByGrenadeState { get; private set; }
    #endregion  

    #region Components
    public Core Core { get; private set; }
    public Animator animator { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Vector2 CurrectVelocity { get; private set; }
    public PlayerInputHandler InputHandeler { get; private set; }
    public InteractableDetector InteractableDetector { get; private set; }
    public Stats Stats { get; private set; }
    #endregion

    #region Other Variable
    [SerializeField]
    private PlayerData playerData;
    private Vector2 workSpace;
    private Weapon Primaryweapon;
    private Weapon Secondtyweapon;
    #endregion 
    #region Unity CallBack Function 
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        stateMachine = new PlayerStateMachine();
        Primaryweapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        Secondtyweapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
        Primaryweapon.SetCore(Core);
        Secondtyweapon.SetCore(Core);
        InteractableDetector = Core.getCoreComponents<InteractableDetector>();
        IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
        LandState = new PalyerLandState(this, stateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlidState(this, stateMachine, playerData, "wallSlide");
        WallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallClimb");
        WallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");
        WallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerData, "ledgeClimbState");
        CrouchIdleState = new PlayerCrouchIdleState(this, stateMachine, playerData,"crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, stateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "primaryattack", Primaryweapon, CombatInputs.primary);
        SecondryAttackState = new PlayerAttackState(this, stateMachine, playerData, "secondryattack", Secondtyweapon, CombatInputs.secondry);

        DeadState = new PlayerDeadState(this, stateMachine, playerData, "IsDead");
        KnockBackState = new PlayerKnockBackState(this, stateMachine, playerData,"knockback");
        PlayerKnockByGrenadeState = new PlayerKnockBackByGrenadeState(this, stateMachine, playerData, "knockByGrenade");
        //PlayerStunState = new PlayerStunState(this, stateMachine, playerData, "stun");
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        InputHandeler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        //MovementCollider = GetComponent<BoxCollider2D>();
        InputHandeler.OnInteractInputChanged += InteractableDetector.TryInteract;
       // Stats.Poise.OnCurrentValueZero += HandlePoiseCurrentValueZero;
        stateMachine.Initilize(IdleState);

    }
    private void HandlePoiseCurrentValueZero()
    {
        stateMachine.ChangeState(PlayerStunState);
    }
    private void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    private void OnDestroy()
    {
       // Stats.Poise.OnCurrentValueZero -= HandlePoiseCurrentValueZero;
    }
    #endregion 
    #region Other Function
    private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishedTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}