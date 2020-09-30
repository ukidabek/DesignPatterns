namespace DesignPatterns.MVC.General
{
    public class ButtonViewModel : Model
    {
        public enum ButtonViewStatus
        {
            Available,
            Unavailable, 
            Hidden
        }

        public object Context { get; set; } = null;
        public ButtonViewStatus Status { get; set; } = ButtonViewStatus.Available;
    }
}