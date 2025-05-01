using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace Tero.CoreSystem
{
    public class Core : MonoBehaviour
    {
        [field: SerializeField] public GameObject Root { get; private set; }
        private readonly List<CoreComponent> coreComponenets = new List<CoreComponent>();
        private void Awake()
        {
            Root = Root ? Root : transform.parent.gameObject;
        }

        public void LogicUpdate()
        {
            foreach (CoreComponent component in coreComponenets)
            {
                component.LogicUpdate();
            }
        }
        public void AddComponent(CoreComponent component)
        {
            if (!coreComponenets.Contains(component))
            {
                coreComponenets.Add(component);
            }
        }

        public T getCoreComponents<T>() where T : CoreComponent
        {
            var comp = coreComponenets.OfType<T>().FirstOrDefault();
            if (comp)
                return comp;

            comp = GetComponentInChildren<T>();
            if (comp)
                return comp;

            Debug.LogWarning($"{typeof(T)} not fount attached on {transform.parent.name}");
            return null;

        }
        public T getCoreComponents<T>(ref T value) where T : CoreComponent
        {
            value = getCoreComponents<T>();
            return value;
        }
    }
}