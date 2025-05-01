using UnityEngine;

namespace Tero.Weapons.Components
{
    public class ProjectileSpawnerData : ComponentData<AttackProjectileSpawner>
    {
        [field: SerializeField] public LayerMask DetectableLayers { get; private set; }
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ProjectileSpawner);
        }
    }
}