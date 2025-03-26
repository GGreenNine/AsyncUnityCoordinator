using System.ComponentModel;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public static class ZenjectExtensions
    {
        public static void BindCoordinatorFactory<T>(this DiContainer container) where T : ICoordinator
        {
            container.BindIFactory<T>().FromMethod(c => (T) CreateCoordinator<T>(c));
        }
        
        private static ICoordinator CreateCoordinator<T>(IInstantiator container) where T : ICoordinator
        {
            return container.Instantiate<T>();
        }
    }
}