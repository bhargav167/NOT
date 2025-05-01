using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="lookForPlayerStateData",menuName ="Data/State Data/LookForPlayer State")]
public class D_LookForPlayer : ScriptableObject
{ 
    public int amountOfTurn=2;
    public float timeBetweenTurns=0.75f;
}
