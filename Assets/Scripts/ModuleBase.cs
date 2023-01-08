using UnityEngine;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public interface IMonoModule<out T> : IReleaseTarget where T : MonoBehaviour
    {
        T Self { get; }
    }
}