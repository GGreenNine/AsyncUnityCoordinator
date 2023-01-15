using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class MetaSceneLoadCoordinator : SceneLoadCoordinator
    {
        public MetaSceneLoadCoordinator(ZenjectSceneLoader zenjectSceneLoader) : base(zenjectSceneLoader) { }

        protected override string SceneToLoad => "MetaScene";
    }
}