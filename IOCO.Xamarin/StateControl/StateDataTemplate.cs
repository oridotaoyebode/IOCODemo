using Xamarin.Forms;

namespace IOCO.Demo.StateControl
{
    [ContentProperty(nameof(Template))]
    public class StateDataTemplate
    {
        private int repeatCount = 1;

        public DataTemplate Template { get; set; }

        public int RepeatCount { get => repeatCount; set => repeatCount = value; }
    }
}
