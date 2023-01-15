using System;
using System.Collections.Generic;
using ModestTree;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public static class CoordinatorTracker
    {
        private static readonly WeakDictionary<ICoordinator, string> Tracking = new();
        private static List<KeyValuePair<ICoordinator, string>> _listPool = new();

        public static void TrackCoordinator(ICoordinator coordinator)
        {
            if (coordinator.Parent != null)
            {
                return;
            }

            var type = coordinator.GetType();
            Tracking.TryAdd(coordinator, type.PrettyName());
        }

        public static void StopTracking(ICoordinator coordinator)
        {
            Tracking.TryRemove(coordinator);
        }

        /// <summary>(trackingId, awaiterType, awaiterStatus, createdTime, stackTrace)</summary>
        public static void ForEachCoordinator(Action<ICoordinator> action)
        {
            lock (_listPool)
            {
                var count = Tracking.ToList(ref _listPool, false);
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        action.Invoke(_listPool[i].Key);
                        _listPool[i] = default;
                    }
                }
                catch
                {
                    _listPool.Clear();
                    throw;
                }
            }
        }
    }
}