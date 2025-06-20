using UnityEngine;

namespace Tero
{
    public class PolicHidePosition : MonoBehaviour
    {
        [SerializeField] public Transform corner1;
        [SerializeField] public Transform corner2;
        public enum HideStatus
        {
            NotHiding,
            MovingToHide,
            Hiding,
            Returning
        }
        public enum HittingDirection
        {
            Front,
            Back
        }
    }
}
