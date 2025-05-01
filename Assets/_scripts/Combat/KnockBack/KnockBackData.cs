using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tero.Combat.KnockBack
{
    public class KnockBackData
    {
        public Vector2 Angle;
        public float Strength;
        public int Direction;
        public float FieldOfImpact;
        public GameObject Source { get; private set; }

        public KnockBackData(Vector2 angle, float strength, int direction, GameObject source, float fieldOfImpact = 0)
        {
            Angle = angle;
            Strength = strength;
            Direction = direction;
            FieldOfImpact = fieldOfImpact;
            Source = source;
        }
    }
}