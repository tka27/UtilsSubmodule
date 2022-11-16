using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExtensionsMain;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UtilsSubmodule
{
    public static class Utils
    {
        public static class Collider
        {
            public static List<UnityEngine.Collider> Overlap(in BoxCollider collider)
            {
                var result = new List<UnityEngine.Collider>();
                result.AddRange(Physics.OverlapBox(collider.bounds.center, collider.bounds.extents,
                    collider.transform.rotation));
                return result.TryRemoveItem(collider, out _);
            }
        }

        public static class Vector3
        {
            public static UnityEngine.Vector3 DisplaceXZ(float range = 1)
            {
                UnityEngine.Vector3 displace = new();
                displace.x = Random.Range(-range, range);
                displace.z = Random.Range(-range, range);
                return displace;
            }

            public static UnityEngine.Vector3 Displace(float range = 1)
            {
                UnityEngine.Vector3 displace = new();
                displace += DisplaceXZ(range);
                displace.y = Random.Range(-range, range);
                return displace;
            }
        }

        public static class Base
        {
            public static bool Roll(int chance)
            {
                int roll = Random.Range(0, 101);
                return roll <= chance;
            }

            public static bool IsActive(Behaviour behaviour)
            {
                if (behaviour == null) return false;
                return behaviour.isActiveAndEnabled;
            }
        }

        public static class Async
        {
            public static async Task<bool> Delay(int delayMS, CancellationToken token)
            {
                await Task.Delay(delayMS);
                return !token.IsCancellationRequested;
            }
        }

        public static class Editor
        {
            /// <summary>
            /// Get all instances of scriptable objects with given type.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T[] GetAllInstances<T>() where T : ScriptableObject
            {
#if UNITY_EDITOR
                return UnityEditor.AssetDatabase.FindAssets($"t: {typeof(T).Name}").ToArray()
                    .Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
                    .Select(UnityEditor.AssetDatabase.LoadAssetAtPath<T>)
                    .ToArray();
#endif
                return null;
            }
        }
    }
}