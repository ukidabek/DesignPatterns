using UnityEngine.Events;

namespace DesignPatterns.MVC.General
{
    public class GeneralEventSignalHandler : SignalHandler
    {
        public UnityEvent OnSignalHandler = new UnityEvent();
        protected override void SignalHandlingLogic(Signal signal) => OnSignalHandler.Invoke();
    }
}