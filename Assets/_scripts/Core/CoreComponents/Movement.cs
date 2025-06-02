using UnityEngine;

namespace Tero.CoreSystem
{
    public class Movement : CoreComponent
    {
        public Rigidbody2D RB { get; private set; }
        public int FacingDirection { get;  set; }
        public bool CanSetVelocity { get; set; }
        public Vector2 CurrectVelocity { get; private set; }
        private Vector2 workSpace;
        private float fixedscaledCharacterY { get; set; }
        private float fixedflipscaledCharacterX { get; set; }
        protected override void Awake()
        {
            base.Awake();
            RB = GetComponentInParent<Rigidbody2D>();
            FacingDirection = 1;
            fixedscaledCharacterY = RB.transform.localScale.y;
            fixedflipscaledCharacterX = RB.transform.localScale.x;
            CanSetVelocity = true;
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            CurrectVelocity = RB.linearVelocity;
        }
        #region SetFunction 
        public void SetVelocityZero()
        {
            workSpace = Vector2.zero;
            SetFinalVelocity();
        }
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            // Set knockback Y angle to some value
            // workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
            // Set knockback Y angle to ZERO
            workSpace.Set(angle.x * velocity * direction, 0f);
            SetFinalVelocity();
        }

        public void SetVelocity(float velocity, Vector2 direction)
        {
            workSpace = direction * velocity;
            SetFinalVelocity();
        }

        public void SetVelocityX(float velocity)
        {
            workSpace.Set(velocity, CurrectVelocity.y);
            SetFinalVelocity();
        }
        public void SetVelocityY(float velocity)
        {
            workSpace.Set(CurrectVelocity.x, velocity);
            SetFinalVelocity();
        }

        //AI JUMP
        public void SetjumpVelocityY(float velocity, float jumpInx, float jumpInY)
        {
            if (FacingDirection == 1)
                workSpace.Set(CurrectVelocity.x * FacingDirection + jumpInx, velocity);
            else
                workSpace.Set(-CurrectVelocity.x * FacingDirection - jumpInx, velocity);

            SetFinalVelocity();
        }
        private void SetFinalVelocity()
        {
            if (CanSetVelocity){
                RB.linearVelocity = workSpace;
                CurrectVelocity = workSpace;
            }
        }
        public void CheckIfShouldFlip(int xInput)
        {
            if (xInput != 0 && xInput != FacingDirection){
                Flip();
            }
        }

        public void Flip()
        {
            FacingDirection *= -1;
            if (FacingDirection == 1)
                RB.transform.localScale = new Vector3(fixedflipscaledCharacterX, fixedscaledCharacterY, 1);
            else
                RB.transform.localScale = new Vector3(-fixedflipscaledCharacterX, fixedscaledCharacterY, 1);
           
        }
        public Vector2 FindRelativePoint(Vector2 offset)
        {
            offset.x *= FacingDirection;

            return transform.position + (Vector3)offset;
        }
        #endregion
    }
}