using UnityEngine;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public interface IMonoModel
    {
        
    }
    
    public interface IMonoModule<in T> : IReleaseTarget where T : IMonoModel
    {
        Transform Self { get; }
        void SetModel(T model);
    }
}