using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class GameEntryPoint : IInitializable, IDisposable
    {
        private readonly IFactory<ICoordinator> _gameCoordinatorFactory;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public GameEntryPoint(IFactory<GameLoadCoordinator> gameCoordinatorFactory)
        {
            _gameCoordinatorFactory = gameCoordinatorFactory;
        }

        public void Initialize()
        {
            InitFlow().Forget();
        }

        private async UniTaskVoid InitFlow()
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token);
            using (cts)
            {
                var gameCoordinator = _gameCoordinatorFactory.Create();
                await gameCoordinator.Present(null, _cts.Token);
            }
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}