using System;
using System.Collections.Generic;
using System.Linq;
using Tero.Weapons.Components;
using Tero.Weapons;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Tero.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : UnityEditor.Editor
    {
        private static List<Type> dataCompTypes = new List<Type>();

        private WeaponDataSO dataSO;

        private void OnEnable()
        {
            dataSO = target as WeaponDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var dataCompType in dataCompTypes)
            {
                if (GUILayout.Button(dataCompType.Name))
                {
                    var comp = Activator.CreateInstance(dataCompType) as ComponentData;

                    if (comp == null)
                        return;

                    dataSO.AddData(comp);
                    EditorUtility.SetDirty(dataSO);
                }
            }
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var filteredTypes = types.Where(
                type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );
            dataCompTypes = filteredTypes.ToList();
        }
    }
}