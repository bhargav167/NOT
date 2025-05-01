using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinateStateMachine{
    public States currentState{get; private set;}
    public void Initilize(States startingState){
        currentState=startingState;
        currentState.Enter();
    }
    public void ChangeState(States newState){
        currentState.Exist();
        currentState=newState;
        currentState.Enter();
    }

}
