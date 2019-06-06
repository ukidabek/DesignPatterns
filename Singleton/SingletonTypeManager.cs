using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace BaseGameLogic.Singleton
{
    [Serializable]
    public abstract class Type
    {
        [SerializeField] private string _name = string.Empty;

        public static bool operator ==(Type a, Type b)
        {
            return a._name == b._name;
        }

        public static bool operator !=(Type a, Type b)
        {
            return a._name != b._name;
        }

        public override bool Equals(object obj)
        {
            return (obj as Type) == this;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public static implicit operator string(Type type)
        {
            return type._name;
        }
    }


    public abstract class SingletonTypeManager<T> : SingletonScriptableObject<T> where T : SingletonScriptableObject<T>
    {
        protected override void Initialize()
        {
            TypesNames = new ReadOnlyCollection<string>(_typesNames);
        }

        [SerializeField] private List<string> _typesNames = new List<string>();
        public ReadOnlyCollection<string> TypesNames { get; private set; }
    }
}