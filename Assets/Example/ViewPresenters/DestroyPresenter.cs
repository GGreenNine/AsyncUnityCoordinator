using System;
using System.Threading;
using UnityEngine;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public abstract class DestroyPresenter : MonoBehaviour, IReleaseTarget
    {
        protected readonly CancellationTokenSource OnDestoryTS = new();

        public IReleaseStrategy ReleaseStrategy
        {
            get
            {
                try
                {
                    var exist = gameObject.TryGetComponent<DestroyReleaseStrategy>(out var component);
                    if (exist)
                    {
                        return component;
                    }

                    return gameObject.AddComponent<DestroyReleaseStrategy>();
                }
                catch (MissingReferenceException)
                {
                    return null;
                }

            }
        }

        private void OnDestroy()
        {
            OnDestoryTS.Cancel();
            OnDestoryTS.Dispose();
        }
    }
}