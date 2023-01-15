using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class GameEntryPoint : IInitializable, IDisposable
    {
        private readonly IFactory<ICoordinator> _gameCoordinatorFactory;
        private readonly CancellationTokenSource _cts = new();
        private readonly IAsyncRouter _router;

        public GameEntryPoint(IFactory<GameLoadCoordinator> gameCoordinatorFactory, IAsyncRouter router)
        {
            _gameCoordinatorFactory = gameCoordinatorFactory;
            _router = router;
        }

        public void Initialize()
        {
            InitFlow().Forget();
        }

        private async UniTaskVoid InitFlow()
        {
            var gameCoordinator = _gameCoordinatorFactory.Create();
            await _router.Transition(null, null, gameCoordinator, _cts.Token);
            gameCoordinator.Dismiss();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}