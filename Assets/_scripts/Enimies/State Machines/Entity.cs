using UnityEngine;
using Tero.CoreSystem;
public class Entity : MonoBehaviour
{
    protected Movement Movement { get => movement ?? Core.getCoreComponents(ref movement); }
    private Movement movement;
    public FinateStateMachine stateMachine { get; set; }
    public Core Core { get; private set; }
    public D_Entity entityData;
    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform leadgerCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform GroundCheck;
    [Header("Settings")]
    [SerializeField] private float hideDistance = 2f;
    [SerializeField] public float hideSpeed = 5f;
    [SerializeField] private float hideOffset = 0.3f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask hideObjectLayer;

    [Header("References")]
    [SerializeField] public Transform policeTransform;
    RaycastHit2D closestHit = new RaycastHit2D();
    [SerializeField] public Transform[] hidePosition;
    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
        stateMachine = new FinateStateMachine();
        atsm = GetComponent<AnimationToStateMachine>();
        Core = GetComponentInChildren<Core>();
    }
    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
        anim.SetFloat("yVelocity", Movement.RB.linearVelocity.y);
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(GroundCheck.position, GroundCheck.position + (Vector3)(Vector2.down * entityData.minAgroDistance));
            Gizmos.DrawLine(leadgerCheck.position, leadgerCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
            // Gizmos.color = Color.yellow;
            // Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.up * 1.3f), 0.1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * hideDistance);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * hideDistance);

        }
    }
    public virtual bool CheckIsGrounded()
    {
        return Physics2D.Raycast(GroundCheck.position, Vector2.down, entityData.minAgroDistance, entityData.whatIsGround);
    }
    public virtual bool CheckPlayerInFrontMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.right * Movement.FacingDirection, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInBackMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, -Vector2.right * Movement.FacingDirection, 1.7f, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInUpMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.up, 1.3f, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.right * Movement.FacingDirection, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.right * Movement.FacingDirection, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    public RaycastHit2D GetClosestHitFromPlayerCheck()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(
            playerCheck.position,
            Vector2.left,
            hideDistance,
            hideObjectLayer
        );

        RaycastHit2D hitRight = Physics2D.Raycast(
            playerCheck.position,
            Vector2.right,
            hideDistance,
            hideObjectLayer
        );
        if (hitLeft && hitRight)
        {
            closestHit = hitLeft.distance < hitRight.distance ? hitLeft : hitRight;
        }
        else if (hitLeft)
        {
            closestHit = hitLeft;
        }
        else if (hitRight)
        {
            closestHit = hitRight;
        }
        return closestHit;
    }
    public void MoveToHidePosition()
    {
            if (movement.FacingDirection == 1)
        {
            policeTransform.transform.position = Vector2.MoveTowards(
              policeTransform.transform.position,
              hidePosition[0].transform.position,
              hideSpeed * Time.deltaTime
         );
        }
        if(movement.FacingDirection==-1)
        {
             policeTransform.transform.position = Vector2.MoveTowards(
                 policeTransform.transform.position,
                 hidePosition[1].transform.position,
                 hideSpeed * Time.deltaTime
            );
        }
    }
}