using Tero.Weapons.Components;
using UnityEngine;

namespace Tero.Weapons.Modifiers
{
    public delegate bool ConditionalDelegate(Transform source, out DirectionalInformation directionalInformation);
}