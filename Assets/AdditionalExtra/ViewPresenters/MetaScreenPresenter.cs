using Grigorii.Tatarinov.UnityCoordinator.ViewModels;
using UnityEngine;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class MetaScreenPresenter : DestroyPresenter, IMonoModule<EmptyViewModel>
    {
        public Transform Self => transform;
        public void SetModel(EmptyViewModel model) { }
    }
}