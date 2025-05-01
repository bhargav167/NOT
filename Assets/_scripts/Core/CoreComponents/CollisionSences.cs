using UnityEngine;

namespace Tero.CoreSystem
{
    public class CollisionSences : CoreComponent
    {
        private Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }

        private Movement movement;
        #region Check Transform
        public Transform GroundCheck
        {
            get => GenericsNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
            private set => groundCheck = value;
        }
        public Transform WallCheck
        {
            get => GenericsNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
            private set => wallCheck = value;
        }
        public Transform CeilingCheck
        {
            get => GenericsNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
            private set => ceilingCheck = value;
        }
        public Transform LedgeCheckHorizontal
        {
            get => GenericsNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
            private set => ledgeCheckHorizontal = value;
        }
        public Transform LedgeCheckVertical
        {
            get => GenericsNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
            private set => ledgeCheckVertical = value;
        }
        public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
        public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
        public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ceilingCheck;
        [SerializeField] private Transform ledgeCheckHorizontal;
        [SerializeField] private Transform ledgeCheckVertical;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private LayerMask whatIsGround;
        #endregion

        #region Check Function
        public bool Grounded
        {
            get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        }
        public bool Wall
        {
            get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
        }
        public bool Ceiling
        {
            get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
        }
        public bool LedgeHorizontal
        {
            get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
        }
        public bool LedgeVertical
        {
            get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
        }
        public bool WallBack
        {
            get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround);
        }

        #endregion
    }
}