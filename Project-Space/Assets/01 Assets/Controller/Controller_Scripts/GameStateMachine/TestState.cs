using UnityEngine;
using Zenject;

namespace Controller.StateMachine.State
{
    public class TestState : IGameState
    {
        [Inject] private InputController inputController;

        public void PrepareState() => Debug.Log(inputController);

        //public void UpdateState() => Debug.Log("I'm playing");
    }
}