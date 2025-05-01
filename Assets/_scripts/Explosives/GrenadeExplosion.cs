using Tero.Combat.Damage;
using Tero.Combat.KnockBack;
using Tero.CoreSystem;
using Tero.ProjectileSystem.Components;
using UnityEngine;

namespace Tero
{
    public class GrenadeExplosion : ProjectileComponent
    {
        public float fieldofImpact;
        public float force;
        public LayerMask _layerToHit;
        private HitBox hitBox;
        private float damageValue = 5.0f;
        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            foreach (var hit in hits)
            {
                Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, _layerToHit);
                foreach (var obj in objects)
                {
                    var objectCombat = obj.GetComponentInChildren<KnockBackReceiver>();
                    var damages = obj.GetComponentInChildren<DamageReceiver>();
                    var damagableObject = obj.GetComponentInParent<Transform>().GetComponentInParent<Rigidbody2D>();

                    if (objectCombat?.gameObject.name != null)
                    {
                        damages.Damage(new DamageData(damageValue, objectCombat?.gameObject), hit);
                        //Handle the direction damagable move on grenade blast.
                        if (transform.position.x > damagableObject.transform.position.x)
                        {
                            objectCombat.KnockBackByGrenades(new KnockBackData(new Vector2((float)damagableObject.transform.position.x * -5, 900), force, -(int)damagableObject.transform.position.x * -2, gameObject));
                        }
                        if (transform.position.x < damagableObject.transform.position.x)
                        {
                            objectCombat.KnockBackByGrenades(new KnockBackData(new Vector2((float)damagableObject.transform.position.x * 5, 900), force, (int)damagableObject.transform.position.x * 2, gameObject));
                        }
                    }
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            hitBox = GetComponent<HitBox>();
            hitBox.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fieldofImpact);
        }
    }
}
