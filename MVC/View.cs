using UnityEngine;

namespace DesignPatterns.MVC
{
    public abstract class View : MonoBehaviour
    {
        private object m_object = null;
        public object Object
        {
            get => m_object;
            set
            {
                if (m_object != value)
                {
                    m_object = value;
                    OnObjectChanged();
                }
            }
        }

        public virtual bool AcceptObject(object @object) => true;

        public virtual void  SetObject(object @object) => Object = @object;

        public virtual void OnObjectChanged()
        {
        }
    }

    public abstract class View<ObjectType> : View
    {
        public new ObjectType Object
        {
            get => (ObjectType) base.Object;
            set => base.Object = value;
        }

        public override bool AcceptObject(object @object) => @object is ObjectType;
    }
}
