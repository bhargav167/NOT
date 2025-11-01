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
    public float AnimationRunTime(RuntimeAnimatorController runtimeAnimatorController, string stateName)
    {
        AnimationClip[] clips = runtimeAnimatorController.animationClips;
        if (clips == null) return 0;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == stateName)
                return clip.length;
        }
        return 0;
    }
}