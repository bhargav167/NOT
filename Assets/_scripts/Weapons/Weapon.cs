
using System;
using UnityEngine;
using Tero.Utility;
using Tero.CoreSystem;
using Tero.Utilities;

namespace Tero.Weapons
{
    public class Weapon : MonoBehaviour {
        public event Action<bool> OnCurrentInputChange;
        public event Action<bool> OnCurrentInputFire;

        public event Action OnEnter;
        public event Action OnExit;
        public event Action OnUseInput;
        public event Action OnUseInputFire;

        //Only for Primary Weapon Gun
        public Transform shoulderJoint;
        public Transform playerHeadtransform;
        [SerializeField] public ParticleSystem shootEffect;
        [SerializeField] public Transform shootPoint;
        [SerializeField] public Transform bulletExtractPoint;
        //Only for Primary Weapon Gun END

        [SerializeField] private float attackCounterResetCooldown;
        public bool CanEnterAttack { get; private set; }

        public WeaponDataSO Data { get; private set; }
        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        }

        public bool CurrentInput
        {
            get => currentInput;
            set
            {
                if (currentInput != value)
                {
                    currentInput = value;
                    OnCurrentInputChange?.Invoke(currentInput);
                }
            }
        }
        public bool CurrentFire
        {
            get => currentFire;
            set
            {
                if (currentFire != value)
                {
                    currentFire = value;
                    OnCurrentInputFire?.Invoke(currentFire);
                }
            }
        }


        public float AttackStartTime { get; private set; }

        public Animator anim { get; private set; }
        public GameObject BaseGameObject { get; private set; }

        public AnimationEventHandler EventHandler
        {
            get
            {
                if (!initDone)
                {
                    GetDependencies();
                }

                return eventHandler;
            }
            private set => eventHandler = value;
        }

        public Core Core { get; private set; }
        private bool initDone;
        private AnimationEventHandler eventHandler;

        private int currentAttackCounter;
        private TimeNotifier attackCounterResetTimeNotifier;

        private bool currentInput;
        private bool currentFire;


        public void Enter()
        {
            AttackStartTime = Time.time;

            attackCounterResetTimeNotifier.Disable();
            
            anim.SetBool("active", true);
            anim.SetInteger("counter", currentAttackCounter);
            OnEnter?.Invoke();
        }

        public void SetCore(Core core)
        {
            Core = core;
        }

        public void SetData(WeaponDataSO data)
        {
            Data = data;

            if (Data is null)
                return;

           // ResetAttackCounter();
        }
        public void SetCanEnterAttack(bool value) => CanEnterAttack = value;
        public void Exit()
        {
            anim.SetBool("active", false);
            anim.SetBool("secondryattack", false);
            CurrentAttackCounter++;
            attackCounterResetTimeNotifier.Init(attackCounterResetCooldown);

            OnExit?.Invoke();
        }

        private void Awake()
        {
            GetDependencies();
            attackCounterResetTimeNotifier = new TimeNotifier();
        }

        private void GetDependencies()
        {
            if (initDone)
                return;

            BaseGameObject = GameObject.Find("Player").gameObject;
            anim = BaseGameObject.GetComponent<Animator>();
            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
           
            initDone = true;
        }

        private void Update()
        {
            attackCounterResetTimeNotifier.Tick();
        }

        private void ResetAttackCounter()
        {
            CurrentAttackCounter = 0;
        }

        private void OnEnable()
        {
            EventHandler.OnUseInput += HandleUseInput;
            EventHandler.OnUseFire += HandleUseInputFire;
            attackCounterResetTimeNotifier.OnNotify += ResetAttackCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnUseInput -= HandleUseInput;
            EventHandler.OnUseFire -= HandleUseInputFire;
            attackCounterResetTimeNotifier.OnNotify -= ResetAttackCounter;
        }

        /// <summary>
        /// Invokes event to pass along information from the AnimationEventHandler to a non-weapon class.
        /// </summary>
        private void HandleUseInput() => OnUseInput?.Invoke();
        private void HandleUseInputFire() => OnUseInputFire?.Invoke();
    }
}