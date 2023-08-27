using UnityEngine;

namespace Controller.StateMachine.State
{
    public class TestState2 : IGameState
    {
        public void UpdateState() => Debug.Log("I'm NOT playing");
    }
}