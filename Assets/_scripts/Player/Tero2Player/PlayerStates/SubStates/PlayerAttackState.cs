using Tero.Weapons;
using UnityEngine;

public class PlayerAttackState : PlayerAbitityState
{
    private int inputIndex;
    private Weapon weapon;
    private bool canInterrupt;
    private bool checkFlip;
    private bool checkMove;
    private Player _player;
    private WeaponGenerator weaponGenerator;
    public PlayerAttackState(
         Player player,
         PlayerStateMachine stateMachine,
         PlayerData playerData,
         string animBoolName,
         Weapon weapon,
         CombatInputs input
         )
    : base(player, stateMachine, playerData, animBoolName) {
        this.weapon = weapon;
        _player = player;
        weaponGenerator = weapon.GetComponent<WeaponGenerator>();

        inputIndex = (int)input;

        weapon.OnUseInput += HandleUseInput;
        weapon.OnUseInputFire += HandleUseFire;
        weapon.EventHandler.OnEnableInterrupt += HandleEnableInterrupt;
        weapon.EventHandler.OnFinish += HandleFinish;
        weapon.EventHandler.OnFireFinish += HandleFireFinish;
        weapon.EventHandler.OnFlipSetActive += HandleFlipSetActive;
        weapon.EventHandler.OnMovementSetActive += HandleMovementSetActive;
    }
    private void HandleFlipSetActive(bool value)
    {
        checkFlip = value;
    }
    private void HandleMovementSetActive(bool value)
    {
        checkMove = value;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        var playerInputHandler = Player.InputHandeler;

        var xInput = playerInputHandler.NormInputx;
        var attackInputs = playerInputHandler.AttackInput;

        weapon.CurrentInput = attackInputs[inputIndex];
        if (checkFlip)
        {
            Movement.CheckIfShouldFlip(xInput);
        }
        if (!canInterrupt)
            return;

        if (xInput != 0 || attackInputs[0] || attackInputs[1])
        {
            isAbilityDone = true;
        }
    }

    private void HandleWeaponGenerating()
    {
        stateMachine.ChangeState(Player.IdleState);
    }
    public override void Enter()
    {
        base.Enter();
        weaponGenerator.OnWeaponGenerating += HandleWeaponGenerating;
        canInterrupt = false;
        weapon.Enter();
    }
    public override void Exit()
    {
        base.Exit();
        weaponGenerator.OnWeaponGenerating -= HandleWeaponGenerating;
        weapon.Exit();
    }
    public bool CanTransitionToAttackState() => weapon.CanEnterAttack;
    private void HandleEnableInterrupt() => canInterrupt = true;
    private void HandleUseInput() => Player.InputHandeler.UseAttackInput(inputIndex);
    
    private void HandleUseFire()
    {
        Player.InputHandeler.UseAttackFire();
    }

    private void HandleFinish()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
    private void HandleFireFinish()
    {
       // weapon.shootEffect.Stop();
    }
}