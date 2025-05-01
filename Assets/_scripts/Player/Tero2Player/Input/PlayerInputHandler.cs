 using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {
    public event Action<bool> OnInteractInputChanged;
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputx { get; private set; }
    public int NormInputy { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool[] AttackInput { get; private set; }
    
    public bool IsreadyToFire=false;
    public bool IsreadyToThrow = false;
    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;

    private void Start(){
        playerInput = GetComponent<PlayerInput>();
        int count=Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInput=new bool[count];
       
    }
    private void Update () {
        CheckJumpInputHoldTime ();
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnInteractInputChanged?.Invoke(true);
            return;
        }
        if (context.canceled)
        {
            OnInteractInputChanged?.Invoke(false);
        }
    }
    
    public void OnPrimaryAttack (InputAction.CallbackContext context) {
       
        if(context.started)
        {
            AttackInput[(int)CombatInputs.primary]=true;
        }
        if(context.canceled)
        {
            AttackInput[(int)CombatInputs.primary]=false;
        }
    }
    public void OnSecondryAttack (InputAction.CallbackContext context) {
         if(context.started){
            AttackInput[(int)CombatInputs.secondry]=true;
        }
        if(context.canceled){
            AttackInput[(int)CombatInputs.secondry]=false;
        }
    }
    // Attack InputHandLer
    public void OnGunAttack(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            IsreadyToFire = true;
        }
        if (context.canceled)
        {
            IsreadyToFire = false;
        }
    }
    // Attack InputHandLer
    public void OnGranadeAttack(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            IsreadyToThrow = false;
        }
        if (context.canceled)
        {
            IsreadyToThrow = true;
        }
    }

    public void OnMoveInput (InputAction.CallbackContext context){
         
        RawMovementInput = context.ReadValue<Vector2> ();
        NormInputx=Mathf.RoundToInt(RawMovementInput.x);
        NormInputy=Mathf.RoundToInt(RawMovementInput.y);
    }
    //Attack Movement
    
    public void OnJumpInput (InputAction.CallbackContext context) {
        if (context.started) { 
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled) {
            JumpInputStop = true;
        }
    }
    public void OnGrabInput (InputAction.CallbackContext contex) {
        if (contex.started) {
            GrabInput = true;
        }
        if (contex.canceled) {
            GrabInput = false;
        }
    }
    public void UseJumpInput () => JumpInput = false;
    public void UseAttackInput(int i) => AttackInput[i] = false;
    public void UseAttackFire() => IsreadyToFire = false;
    private void CheckJumpInputHoldTime () {
        if (Time.time >= jumpInputStartTime + inputHoldTime) {
            JumpInput = false;
        }
    }
}

public enum CombatInputs {
    primary,
    secondry
}