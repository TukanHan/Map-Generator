using System;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    [Serializable]
    public abstract class ModelWithPriority<T> where T : IDataModelValidation
    {
        [Range(1, 100)]
        public int priority = 20;

        public T model;
    }
}