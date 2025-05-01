using UnityEngine;
using Tero.CoreSystem;
public class States {
    protected FinateStateMachine stateMachine;
    protected Entity entity;
    protected Core core;
    public float startTime {get;protected set;}
    protected string animBoolName;
    public States (Entity entity, FinateStateMachine stateMachine, string animBoolName){
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        core=entity.Core;
        
    }
    public virtual void Enter () {
        startTime = Time.time;
        entity.anim.SetBool (animBoolName, true);
        DoCheck ();
    }
    public virtual void Exist () {
        entity.anim.SetBool (animBoolName, false);
    }
    public virtual void LogicUpdate () {
         
     }
    public virtual void PhysicsUpdate () {
        DoCheck ();
    }
    public virtual void DoCheck () {}
}