using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceFarm
{
    public abstract class ActionImage : MonoBehaviour
    {
        [SerializeField] protected SeedData _seedData;
        public SeedData SeedData { get => _seedData; }

        public abstract void Execute(Transform target);
    }
}