using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace BaseGameLogic.Singleton
{
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