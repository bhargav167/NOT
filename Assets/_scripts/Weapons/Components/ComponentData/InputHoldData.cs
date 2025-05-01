using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tero.Weapons.Components
{
    public class InputHoldData : ComponentData
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(InputHold);
        }
    }
}
