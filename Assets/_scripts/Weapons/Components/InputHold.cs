using Tero.CoreSystem;
using Tero.ProjectileSystem.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.Windows;

namespace Tero.Weapons.Components
{
    public class InputHold : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
    {
        private Animator anim;
        public Player player;
        public bool input;
        private bool minHoldPassed;
        public float maxTiltAngle = 10f;

        // Movement Core Comp needed to get FacingDirection 
        private CoreSystem.Movement coreMovement;
        private CoreSystem.Movement CoreMovement => coreMovement ? coreMovement : Core.getCoreComponents(ref coreMovement);

        private CoreSystem.KnockBackReceiver combat;
        private CoreSystem.KnockBackReceiver Combat => combat ? combat : Core.getCoreComponents(ref combat);

        private CoreSystem.Death death;
        private CoreSystem.Death Death => death ? death : Core.getCoreComponents(ref death);

        float _offset = -90;
        Vector3 _startingSize;
        Vector3 _armStartingSize;

        private float bulletsPerMinute = 400; // Rounds per minut

        private float fireDelay; // Time between shots in seconds
        private float lastShotTime; // Time when last shot was fired
        private float automaticFireRates = 0.4f;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            minHoldPassed = false;
        }

        private void HandleCurrentInputChange(bool newInput)
        {
            input = newInput;
            SetAnimatorParameter();
        }
        private void HandleCurrentInputFire(bool isFire)
        {
            SetAnimatorParameterFire();
        }
        private void HandleGunRotationWithAim()
        {
            Vector3 mousePosScreen = Mouse.current.position.ReadValue();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosScreen.x, mousePosScreen.y));
            Vector3 perendicular = weapon.shoulderJoint.position - mousePos;
            Quaternion val = Quaternion.LookRotation(Vector3.forward, perendicular);
            val *= Quaternion.Euler(0, 0, _offset);
                weapon.shoulderJoint.rotation = val;
                if (transform.position.x - mousePos.x < 0){
                    if (CoreMovement.FacingDirection == 1){
                        transform.localScale = new Vector3(_startingSize.x, _startingSize.y, _startingSize.z);
                        weapon.shoulderJoint.localScale = new Vector3(_armStartingSize.x, _armStartingSize.y, _armStartingSize.z);
                    }
                }
                if (transform.position.x - mousePos.x > 0){
                    if (CoreMovement.FacingDirection == -1){
                        transform.localScale = new Vector3(_startingSize.x, _startingSize.y, _startingSize.z);
                        weapon.shoulderJoint.localScale = new Vector3(-_armStartingSize.x, -_armStartingSize.y, _armStartingSize.z);
                    }
                }
            //Handle player head lookAt based on mouse position.
            // Get the gun's normalized angle (-180 to 180)
            float gunAngle = NormalizeAngle(weapon.shoulderJoint.eulerAngles.z);
            // Calculate tilt based on gun direction
            float tilt = CalculateTilt(gunAngle);
            // Apply the rotation to the spine
            weapon.playerHeadtransform.localRotation = Quaternion.Euler(0, 0, tilt);
            // To set and adjust direction of projectile bullet --need to adjust hand and wrapper gameobject in editor (Unity)
            currentAttackData.SpawnInfos[0].Direction = val;
        }

        private float CalculateTilt(float gunAngle)
        {
            float tilt = 0f;
            // Pointing upwards (0° to 180° normalized to 0°-90° after clamping)
            if (gunAngle > 0)
            {
                float angleClamped = Mathf.Clamp(gunAngle, 0, 190);
                tilt = Mathf.Lerp(0, maxTiltAngle, angleClamped / 90);
            }
            // Pointing downwards (-180° to 0° normalized to -90°-0° after clamping)
            else
            {
                float angleClamped = Mathf.Clamp(-gunAngle, 0, 190);
                tilt = Mathf.Lerp(0, -maxTiltAngle, angleClamped / 90);
            }

           
            return tilt;
        }

        private float NormalizeAngle(float angle)
        {
            angle %= 360;
            return angle > 180 ? angle - 360 : angle;
        }

        private void HandleMinHoldPassed()
        {
            minHoldPassed = true;
            SetAnimatorParameter();
        }
        private void Update()
        {
            if (Death.IsDead) {
                anim.SetBool("IsDead", true); return; } 

            if (Combat.isKnockBackActive)
                anim.SetBool("knockback", true);
            if (input && weapon.Data.Name == "AK_Gun" && !Combat.isKnockBackActive){
                anim.SetBool("knockback", false);
                HandleGunRotationWithAim();
                if (player.InputHandeler.NormInputx != 0.0f && input){
                    anim.SetBool("shootwalk", true);
                    CoreMovement.SetVelocityX(1.5f * player.InputHandeler.NormInputx);
                    anim.SetLayerWeight(1, 1);
                }
                else
                {
                    anim.SetBool("shootwalk", false);
                    anim.SetLayerWeight(1, 0);
                }
            }
                SetAnimatorParameterFire();
        }
        private void SetAnimatorParameter()
        {
            if (input){
                anim.SetBool("hold", input);
                return;
            }
            if (minHoldPassed || !input)
            {
                anim.SetBool("hold", false);
                if (weapon.Data.Name == "AK_Gun")
                    weapon.EventHandler.AnimationFinishedTrigger();
            }
        }
       
        private void SetAnimatorParameterFire()
        {
            if (input && player.InputHandeler.IsreadyToFire)
            {
                automaticFireRates -= Time.deltaTime;
                if (automaticFireRates > 0)
                {
                    // Check if enough time has passed since last shot
                    if (Time.time - lastShotTime >= fireDelay)
                    {
                        anim.SetBool("fire", player.InputHandeler.IsreadyToFire);
                        lastShotTime = Time.time;
                        return;
                    }
                }
            }
            else
            {
                automaticFireRates = 0.4f;
            }
            if (minHoldPassed || !player.InputHandeler.IsreadyToFire)
            {
                anim.SetBool("fire", false);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            player = GetComponentInParent<Player>();
            anim = GetComponentInParent<Animator>();
            _startingSize = transform.localScale;
            _armStartingSize = weapon.shoulderJoint.localScale;
            weapon.OnCurrentInputChange += HandleCurrentInputChange;
            weapon.OnCurrentInputFire += HandleCurrentInputFire;
            AnimationEventHandler.OnMinHoldPassed += HandleMinHoldPassed;
            // Calculate fire delay based on bullets per minute
            if (bulletsPerMinute > 0){
                fireDelay = 60f / bulletsPerMinute;
            }
            else
            {
                fireDelay = 0.1f; // Default value if BPM is zero
                Debug.LogWarning("Bullets per minute should be greater than zero");
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
            weapon.OnCurrentInputFire -= HandleCurrentInputFire;
            AnimationEventHandler.OnMinHoldPassed -= HandleMinHoldPassed;
        }
    }
}