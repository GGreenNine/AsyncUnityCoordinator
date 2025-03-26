using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Grigorii.Tatarinov.UnityCoordinator.ViewModels
{
    public class ConfirmationViewModel : IMonoModel
    {
        public IUniTaskAsyncEnumerable<AsyncUnit> OnAcceptClick;
        public IUniTaskAsyncEnumerable<AsyncUnit> OnDismissClick;

    }
}