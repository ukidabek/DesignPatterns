using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DesignPatterns.MVC.General
{
    public class GeneralListView : View<IList>
    {
        [SerializeField] private RectTransform m_parent = null;
        [SerializeField] private ContextView[] m_prefabs = null;
        
        private readonly List<ContextView> m_instances = new List<ContextView>();
        
        public override void OnObjectChanged()
        {
            foreach (var instance in m_instances)
                Destroy(instance.gameObject);
            m_instances.Clear();
            foreach (var @object in Object)
            {
                ContextView view = m_prefabs.FirstOrDefault(contextView => contextView.AcceptObject(@object));
                m_instances.Add(Instantiate(view, m_parent, false));
                m_instances.Last().SetObject(@object);
            }
        }
    }
}
