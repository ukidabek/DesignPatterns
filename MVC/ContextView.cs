using System.Linq;
using UnityEngine;

namespace DesignPatterns.MVC
{
    public class ContextView : MonoBehaviour
    {
        private View[] m_views = null;
        private object m_context;

        private void Awake() => GetViews();
        public bool AcceptObject(object @object)
        {
            GetViews();
            foreach (var VARIABLE in m_views)
            {
                if (!VARIABLE.AcceptObject(@object))
                    return false;
            }

            return true;
        }

        private void GetViews()
        {
            if(m_views == null || m_views.Length == 0)
                m_views = GetComponents<View>();
        }

        public void OnObjectChanged()
        {
            GetViews();
            foreach (var view in m_views)
                view.SetObject(m_context);
        }

        public void SetObject(object context)
        {
            if (m_context != context)
            {
                m_context = context;
                OnObjectChanged();
            }
        }
    }
}
