using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public abstract class SceneLoadCoordinator : AbstractCoordinator
    {
        private readonly ZenjectSceneLoader _zenjectSceneLoader;
        
        // you can specify more parameters if you need
        protected abstract string SceneToLoad { get; }

        protected SceneLoadCoordinator(ZenjectSceneLoader zenjectSceneLoader)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
        }

        protected override async UniTask<IRouteResult> OnPresent(CancellationToken cancellationToken)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            
            try
            {
                await _zenjectSceneLoader.LoadSceneAsync(SceneToLoad).ToUniTask(cancellationToken: cts.Token);
                return new RouteSuccessResult();
            }
            catch (Exception)
            {
                return new RouteFailedResult();
            }
        }

    }
}