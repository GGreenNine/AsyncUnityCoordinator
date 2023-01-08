using System;
using System.Threading;
using UnityEngine;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public abstract class DestroyPresenter : MonoBehaviour, IReleaseTarget
    {
        protected readonly CancellationTokenSource OnDestoryTS = new CancellationTokenSource();
        public IReleaseStrategy ReleaseStrategy
        {
            get
            {
                var component = gameObject.GetComponent<DestroyReleaseStrategy>();
                if (component != null)
                {
                    return component;
                }

                return gameObject.AddComponent<DestroyReleaseStrategy>();
            }
        }

        private void OnDestroy()
        {
            OnDestoryTS.Cancel();
            OnDestoryTS.Dispose();
        }
    }
}