using UnityEngine;

namespace DesignPatterns.MVC
{
    public abstract class SignalHandler : MonoBehaviour
    {
        [SerializeField] private SignalType m_signalType = null;

        protected abstract void SignalHandlingLogic(Signal signal);
        public virtual bool HandleSignal(Signal signal)
        {
            if (signal.Type == m_signalType)
            {
                SignalHandlingLogic(signal);
                return true;
            }

            return false;
        }
    }
}