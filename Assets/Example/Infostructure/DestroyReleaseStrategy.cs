using UnityEngine;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class DestroyReleaseStrategy : MonoBehaviour, IReleaseStrategy
    {
        public void Release()
        {
            Destroy(gameObject);
        }
    }
}