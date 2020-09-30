using System;
using UnityEngine;

namespace DesignPatterns.Singleton
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

        public override string ToString()
        {
            return _name;
        }
    }
}