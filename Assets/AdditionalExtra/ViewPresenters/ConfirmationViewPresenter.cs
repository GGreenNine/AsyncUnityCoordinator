using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class ConfirmationViewPresenter : DestroyPresenter, IMonoModule<ConfirmationViewPresenter>
    {
        public ConfirmationViewPresenter Self => this;

        public async UniTask OnAcceptClick(CancellationToken token)
        {
            var combinedCTS = CancellationTokenSource.CreateLinkedTokenSource(token, OnDestoryTS.Token);
            await confirmationButton.OnClickAsync(combinedCTS.Token);
        }
        
        public async UniTask OnDismissClick(CancellationToken token)
        {
            var combinedCTS = CancellationTokenSource.CreateLinkedTokenSource(token, OnDestoryTS.Token);
            await dismissButton.OnClickAsync(combinedCTS.Token);
        }
        
        [SerializeField] private Button confirmationButton;
        [SerializeField] private Button dismissButton;
    }
}