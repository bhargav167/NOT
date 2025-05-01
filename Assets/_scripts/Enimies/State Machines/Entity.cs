using UnityEngine;
using Tero.CoreSystem;
public class Entity : MonoBehaviour {
    protected Movement Movement {get=>movement ?? Core.getCoreComponents(ref movement);} 
    private Movement movement;
    public FinateStateMachine stateMachine { get; set; }
    public Core Core{get;private set;}
    public D_Entity entityData;
    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set; } 
    private Vector2 velocityWorkspace;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform leadgerCheck;
    [SerializeField]
    private Transform playerCheck;
     [SerializeField]
    private Transform GroundCheck;
    private bool isgrounded = false;
    public virtual void Awake (){
        anim = GetComponent<Animator> ();
        stateMachine = new FinateStateMachine ();
        atsm = GetComponent<AnimationToStateMachine> ();
        Core= GetComponentInChildren<Core>();
    }
    public virtual void Update (){
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate ();
        anim.SetFloat("yVelocity",Movement.RB.velocity.y);
    }
    public virtual void FixedUpdate () {
        stateMachine.currentState.PhysicsUpdate ();
        isgrounded = CheckIsGrounded();

    }
    public virtual void OnDrawGizmos(){
        if (Core != null){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(GroundCheck.position, GroundCheck.position + (Vector3)(Vector2.down * entityData.minAgroDistance));
            Gizmos.DrawLine(leadgerCheck.position, leadgerCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
            Gizmos.color = Color.yellow;
           Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.up * 1.3f), 0.1f);
           
        }
    }
    public virtual bool CheckIsGrounded()
    {
        return Physics2D.Raycast(GroundCheck.position, Vector2.down , entityData.minAgroDistance, entityData.whatIsGround);
    }
    public virtual bool CheckPlayerInFrontMinAgroRange () {
        return Physics2D.Raycast (playerCheck.position, Vector2.right * Movement.FacingDirection, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInBackMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, -Vector2.right * Movement.FacingDirection, 1.7f, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInUpMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.up, 1.3f, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange () {
        return Physics2D.Raycast (playerCheck.position, Vector2.right * Movement.FacingDirection, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction () { 
        return Physics2D.Raycast (playerCheck.position, Vector2.right * Movement.FacingDirection, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    } 
}