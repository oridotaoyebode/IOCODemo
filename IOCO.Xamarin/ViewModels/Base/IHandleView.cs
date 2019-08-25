using System.Threading.Tasks;
using Xamarin.Forms;

namespace IOCO.Xamarin.ViewModels.Base
{
    public interface IHandleViewAppearing
    {
        Task OnViewAppearingAsync(VisualElement view);
    }

    public interface IHandleViewDisappearing
    {
        Task OnViewDisappearingAsync(VisualElement view);
    }
}