namespace DesignPatterns.MVC
{
    public class Signal
    {
        public readonly object Context;
        public readonly SignalType Type;

        public Signal(object context, SignalType type)
        {
            Context = context;
            Type = type;
        }
    }
}