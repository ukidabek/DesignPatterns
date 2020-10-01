using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace DesignPatterns.MVC
{
    public abstract class Controller : MonoBehaviour
    {
        [SerializeField] private View[] m_views = null;
        [SerializeField] private SignalHandler[] m_signalHandlers = null;
        
        private Model m_model = null;
        protected Model Model
        {
            get
            {
                if (m_model == null)
                    m_model = CreateModel();
                return m_model;
            }
        }
        
        protected abstract Model CreateModel();

        private void OnEnable()
        {
            foreach (var view in m_views)
                if (view.AcceptObject(Model))
                    view.SetObject(Model);
        }

        private void OnDisable() => DisposeModel();

        private void DisposeModel()
        {
            Model?.Dispose();
            m_model = null;
        }

        private void OnDestroy()=> DisposeModel();
        

        public void HandleSignal(Signal signal)
        {
            foreach (var signalHandler in m_signalHandlers)
            {
                if(signalHandler.HandleSignal(signal))
                    break;
            }
        }
    }

    public abstract class Controller<ModelType> : Controller where ModelType : Model
    {
        protected override Model CreateModel() => Model.Create<ModelType>();
    }
}