using System;
using UnityEngine;

namespace DesignPatterns.MVC
{
    public abstract class Model : ScriptableObject, IDisposable
    {
        public virtual void Initialize(){}

        public static T Create<T>(Action<T> onModelCrate = null) where T : Model
        {
            T model = CreateInstance<T>();
            onModelCrate?.Invoke(model);
            model.Initialize();
            return model;
        }

        public virtual void Dispose() {}
    }
}