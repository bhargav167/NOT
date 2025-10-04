using UnityEngine;

namespace Tero
{
    [CreateAssetMenu(fileName = "D_HideStateData", menuName = "Scriptable Objects/D_HideStateData")]
    public class D_HideStateData : ScriptableObject
    {
        public float hideDistanceFront = 1f;
        public float hideDistanceBack = 1f;
        public float hideSpeed = 5f;
        public float hideOffset =  .001f;
        public bool flippedToHide = false;
    }
}
