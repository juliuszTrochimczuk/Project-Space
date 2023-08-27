using Controller;
using Zenject;

namespace Installer
{
    public class ControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputController>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Controller.StateMachine.GameStateMachine>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Controller.StateMachine.State.TestState>().AsSingle();
            Container.Bind<Controller.StateMachine.State.TestState2>().AsSingle();
        }
    }
}