using UnityEngine;
using Zenject;

namespace Installer
{
    public class ControllerInstaller : MonoInstaller
    {
        [SerializeField] private InputController inputController;

        public override void InstallBindings() => Container.Bind<InputController>().FromInstance(inputController).AsSingle();
    }
}