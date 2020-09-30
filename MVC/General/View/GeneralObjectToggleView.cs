using UnityEngine;

namespace DesignPatterns.MVC.General
{
    public class GeneralObjectToggleView : GeneralPropertyView
    {
        [SerializeField] private GameObject[] m_objectToActivate = null;
        [SerializeField] private GameObject[] m_objectToDeactivate = null;
        public override bool AcceptObject(object @object) => base.AcceptObject(@object) && IsPropertyType<bool>(@object);

        public override void OnObjectChanged()
        {
            bool status = GetValue<bool>();
            foreach (var o in m_objectToActivate)
                o.SetActive(status);
            foreach (var o in m_objectToDeactivate)
                o.SetActive(!status);

        }
    }
}