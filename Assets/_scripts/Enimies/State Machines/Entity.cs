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
    public D_HideStateData hideStateData;
    public Animator anim { get; set; }
    public AnimationToStateMachine atsm { get; private set; }
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform leadgerCheck;
    [SerializeField]
    private Transform playerCheck;
    [Header("Settings")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask hideObjectLayer;
    [Header("References")]
    [SerializeField] public Transform policeTransform;
    private Player _player;
    public Vector2 originalPosition;
    RaycastHit2D closestHit = new RaycastHit2D();
    public HideStatus currentHideStatus = HideStatus.NotHiding;
    public HideStatus CurrentHideStatus => currentHideStatus;
    public HittingDirection currentHittingDirection = HittingDirection.Front;
    public HittingDirection CurrentHittingDirection => currentHittingDirection;
    private bool _IsActivatedtoHide = false;
    public bool _IsMovedtoOrignalPos = false;
    public bool isfacingPlayer = false;
    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
        stateMachine = new FinateStateMachine();
        atsm = GetComponent<AnimationToStateMachine>();
        Core = GetComponentInChildren<Core>();
        _player = FindAnyObjectByType<Player>();
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
        if (Core.getCoreComponents<KnockBackReceiver>().isKnockBackActive)
        {
            _IsActivatedtoHide = true;
        }
        //Debug.Log(_player.GetComponentInChildren<Core>().GetComponentInChildren<Stats>().Health.CurrentValue);
        //Debug.Log(_player.playerCurrentState);
        //Debug.Log(Core.getCoreComponents<KnockBackReceiver>().isKnockBackActive);
        //Debug.Log(Core.getCoreComponents<Stats>().Health.CurrentValue);

    }
    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            Gizmos.color = Color.red;
           // Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.down * entityData.minAgroDistance));
            Gizmos.DrawLine(leadgerCheck.position, leadgerCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * Movement.FacingDirection * hideStateData.hideDistanceFront);
            Gizmos.color = Color.darkBlue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * Movement.FacingDirection * hideStateData.hideDistanceBack);
            Gizmos.color = Color.indianRed;
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * entityData.playercheckRayDistance));
            Gizmos.color = Color.greenYellow;
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.playercheckRayDistance));
        }
    }
    #region castingRays
    public virtual bool CastRayForPlayerCheck()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.right * Movement.FacingDirection, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool StaticCastRayForPlayerRight()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.right, entityData.minAgroDistance, entityData.whatIsPlayer);
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
    #endregion
    protected internal RaycastHit2D GetClosestHitFromPlayerCheck()
    {
        if (_IsActivatedtoHide)
        {
            RaycastHit2D hitFront = Physics2D.Raycast(
               playerCheck.position,
               Vector2.right * Movement.FacingDirection,
               hideStateData.hideDistanceFront,
               hideObjectLayer
           );
            RaycastHit2D hitBack = Physics2D.Raycast(
                playerCheck.position,
                Vector2.left * Movement.FacingDirection,
                hideStateData.hideDistanceBack,
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
                _IsActivatedtoHide = false;
                return closestHit;
            }
        }

        return closestHit;
    }
    protected internal void MoveToHidePosition(bool isPlayerRight)
    {
        if (closestHit && currentHideStatus != HideStatus.Hiding && currentHideStatus != HideStatus.Returning)
        {
            var hideObject = closestHit.collider.gameObject;
            if (hideObject != null)
            {
                    policeTransform.transform.position = Vector2.MoveTowards(
                      policeTransform.transform.position,
                     !isPlayerRight? hideObject.GetComponent<PolicHidePosition>().corner1.transform.position : hideObject.GetComponent<PolicHidePosition>().corner2.transform.position,
                      hideStateData.hideSpeed * Time.deltaTime  
                    );
            }
            if (Vector2.Distance(policeTransform.transform.position, hideObject.GetComponent<PolicHidePosition>().corner2.transform.position) <= 0.2f 
              || Vector2.Distance(policeTransform.transform.position, hideObject.GetComponent<PolicHidePosition>().corner1.transform.position) <= 0.2f){
                currentHideStatus = HideStatus.Hiding;
                hideStateData.flippedToHide = false;
            }
        }
    }
    protected internal void ReturnToOriginalPosition()
    {
        if (currentHideStatus == HideStatus.Returning)
        {
            Flip();
            anim.SetBool("returning", true);
            policeTransform.transform.position = Vector2.MoveTowards(
                policeTransform.transform.position,
                originalPosition,
                hideStateData.hideSpeed * Time.deltaTime
            );
            if (Vector2.Distance(policeTransform.transform.position, originalPosition) < 0.1f)
            {
                hideStateData.flippedToHide = false;
                currentHideStatus = HideStatus.NotHiding;
                isfacingPlayer = CastRayForPlayerCheck();
                _IsMovedtoOrignalPos = true;
                Flip();
                anim.SetBool("returning", false);
            }
        }
    }
    protected internal void Flip()
    {
        if (hideStateData.flippedToHide || isfacingPlayer) return;

        hideStateData.flippedToHide = true;
        Movement.Flip();
    }
}