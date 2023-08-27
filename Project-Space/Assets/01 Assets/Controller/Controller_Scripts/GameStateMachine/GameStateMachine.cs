using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Controller.StateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        public enum GameStateId { Play, Pause, Menu}

        private List<IGameState> states = new List<IGameState>();

        [Inject] private State.TestState testState;
        [Inject] private State.TestState2 testState2;

        public GameStateId currentStateId;

        private void Awake()
        {
            states.Add(testState);
            states.Add(testState2);
        }

        private void Start() => states[(int)currentStateId].PrepareState();

        private void Update() => states[(int)currentStateId].UpdateState();

        public void ChangeState(GameStateId newStateId)
        {
            states[(int)currentStateId].DestroyState();
            currentStateId = newStateId;
            states[(int)currentStateId].PrepareState();
        }
    }
}