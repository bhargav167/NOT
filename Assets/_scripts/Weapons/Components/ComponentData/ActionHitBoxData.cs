using Tero.Weapons.Components;
using UnityEngine;

namespace Tero.Weapons.Components
{
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {
        [field: SerializeField] public LayerMask DetectableLayers { get; private set; }
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ActionHitBox);
        }
    }
}