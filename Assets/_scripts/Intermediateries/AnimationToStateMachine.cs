using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationToStateMachine : MonoBehaviour {
    public AttactState attackState;

    private void TriggerAttack()
    {
        attackState.TriggerAttact();
    }

    private void FinishAttack()
    {
        attackState.FinishAttact();
    }
    private void FinishGranadeKnockback()
    {
        attackState.FinishGranadeKnockback();
    }

    private void SetParryWindowActive(int value)
    {
       // attackState.SetParryWindowActive(Convert.ToBoolean(value));
    }
}