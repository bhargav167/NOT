using System;
using Tero.CoreSystem;
using UnityEngine;

namespace Tero.ProjectileSystem.Components
{
    /// <summary>
    /// The Movement projectile component is responsible for applying a velocity to the projectile. The velocity can be applied only once upon the projectile
    /// being fired, or can be applied continuously as if self powered. 
    /// </summary>
    public class Movement : ProjectileComponent
    {
        [field: SerializeField] public bool ApplyContinuously { get; private set; }
        [field: SerializeField] public bool IsbulletExtracter { get; private set; }
        [field: SerializeField] public float Speed { get;  set; }
        [field: SerializeField] public float ExtracterKnockback { get; private set; }
        private float timetoRotate = 0.0f;
        // On Init, set projectile velocity once
        protected override void Init()
        {
            base.Init();
            SetVelocity();
            timetoRotate = UnityEngine.Random.Range(0.3f, 0.8f);
            if (IsbulletExtracter)
            {
                SetExtractAngle();
            } 
        }
        private void SetExtractAngle()
        {
            rb.AddForceX(ExtracterKnockback);
        }
        private void SetExtractRotation()
        {
            rb.MoveRotation(rb.rotation * 8);
        }
        private void SetVelocity() {
            rb.velocity = Speed * transform.right;
        } 

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (IsbulletExtracter && !projectile.IsBulletGrounded)
            {
                SetExtractRotation();
            }
            if (projectile.IsBulletGrounded)
            {
                timetoRotate -= Time.deltaTime;

                if(timetoRotate>0)
                rb.MoveRotation(rb.rotation * 5);
            }
            if (!ApplyContinuously)
                return;

            SetVelocity();
        }
    }
}