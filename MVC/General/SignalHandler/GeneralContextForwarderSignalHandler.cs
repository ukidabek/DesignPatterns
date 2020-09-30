using UnityEngine;

namespace DesignPatterns.MVC.General
{
    public class GeneralContextForwarderSignalHandler : SignalHandler
    {
        [SerializeField] private ContextView m_contest = null;
        
        protected override void SignalHandlingLogic(Signal signal)
        {
            if (m_contest != null)
                m_contest?.SetObject(signal.Context);
        }
    }
}