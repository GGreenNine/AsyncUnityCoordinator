using System.Threading;
using Cysharp.Threading.Tasks;
using Grigorii.Tatarinov.UnityCoordinator.ViewModels;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class ConfirmationViewPresenter : DestroyPresenter, IMonoModule<ConfirmationViewModel>
    {
        [SerializeField] private Button confirmationButton;
        [SerializeField] private Button dismissButton;
        public Transform Self => transform;
        public void SetModel(ConfirmationViewModel model)
        {
            model.OnAcceptClick = confirmationButton.OnClickAsAsyncEnumerable();
            model.OnDismissClick = dismissButton.OnClickAsAsyncEnumerable();
        }
    }
}