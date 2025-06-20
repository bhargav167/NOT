using UnityEngine;
using Tero.CoreSystem;
using System;
using Tero;
using static Tero.PolicHidePosition;
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
    [SerializeField] private float hideDistanceFront = 1f;
    [SerializeField] private float hideDistanceBack = 1f;
    [SerializeField] public float hideSpeed = 5f;
    [SerializeField] private float hideOffset = 0.3f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask hideObjectLayer;

    [Header("References")]
    [SerializeField] public Transform policeTransform;
    public Vector2 originalPosition;
    RaycastHit2D closestHit = new RaycastHit2D();
    public HideStatus currentHideStatus = HideStatus.NotHiding;
    public HideStatus CurrentHideStatus => currentHideStatus;
    public HittingDirection currentHittingDirection = HittingDirection.Front;
    public HittingDirection CurrentHittingDirection => currentHittingDirection;

    public bool flippedToHide = false;
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
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * Movement.FacingDirection * hideDistanceFront);
            Gizmos.color = Color.darkBlue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * Movement.FacingDirection * hideDistanceBack);

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
        RaycastHit2D hitFront = Physics2D.Raycast(
            playerCheck.position,
            Vector2.right * Movement.FacingDirection,
            hideDistanceFront,
            hideObjectLayer
        );
        RaycastHit2D hitBack = Physics2D.Raycast(
            playerCheck.position,
            Vector2.left * Movement.FacingDirection,
            hideDistanceBack,
            hideObjectLayer
        );

        if (hitFront)
        {
            currentHittingDirection = HittingDirection.Front;
            return closestHit = hitFront;
        }
        if (hitBack)
        {
            currentHittingDirection = HittingDirection.Back;
            return closestHit = hitBack;
        }
        if (hitFront && hitBack)
        {
            if (Vector2.Distance(playerCheck.position, hitFront.point) < Vector2.Distance(playerCheck.position, hitBack.point))
            {
                return closestHit = hitFront;
            }
            else
            {
                return closestHit = hitBack;
            }
        }
        if (hitFront.collider == null && hitBack.collider == null)
        {
            closestHit = new RaycastHit2D();
            return closestHit;
        }
        return closestHit;
    }
    public void MoveToHidePosition()
    {
        if (currentHideStatus != HideStatus.Hiding && currentHideStatus != HideStatus.Returning)
        {
            var hideObject = closestHit.collider.gameObject;
            if (hideObject != null)
            {
                if (movement.FacingDirection == -1)
                {
                    policeTransform.transform.position = Vector2.MoveTowards(
                      policeTransform.transform.position,
                      hideObject.GetComponent<PolicHidePosition>().corner1.transform.position,
                      hideSpeed * Time.deltaTime
                 );
                }
                if (movement.FacingDirection == 1)
                {
                    policeTransform.transform.position = Vector2.MoveTowards(
                        policeTransform.transform.position,
                        hideObject.GetComponent<PolicHidePosition>().corner2.transform.position,
                        hideSpeed * Time.deltaTime
                   );
                }
            }
            if (Vector2.Distance(policeTransform.transform.position, hideObject.GetComponent<PolicHidePosition>().corner2.transform.position) <= 0.2f || Vector2.Distance(policeTransform.transform.position, hideObject.GetComponent<PolicHidePosition>().corner1.transform.position) <= 0.2f)
            {
                currentHideStatus = HideStatus.Hiding;
                flippedToHide = false;
            }
        }

    }
    public void ReturnToOriginalPosition()
    {
        currentHideStatus = HideStatus.Returning;
        policeTransform.transform.position = Vector2.MoveTowards(
            policeTransform.transform.position,
            originalPosition,
            hideSpeed * Time.deltaTime
        ); 
        if (Vector2.Distance(policeTransform.transform.position, originalPosition) < 0.1f)
        {
           currentHideStatus = HideStatus.NotHiding;
        }
    }
}