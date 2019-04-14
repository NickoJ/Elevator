using System.Collections.Generic;

namespace Klyukay.Lift.Models
{

    public class LiftManager : ILiftManager, ICommandReceiver, ITickable
    {

        private Lift _lift;
        private Floor[] _floors;
        private Floor _currentFloor;
        
        public LiftManager(int floorsCount)
        {
            _floors = new Floor[floorsCount];
            
            for (int i = 0; i < floorsCount; i++)
            {
                _floors[i] = new Floor(i + 1, this);
            }

            _currentFloor = _floors[0];
            _lift = new Lift(_currentFloor.Number);
            _lift.OnFloorChanged += LiftFloorChanged;
            _lift.OnStateChanged += LiftStateChanged;
            LiftStateChanged(_lift.State);
        }

        public IFloor CurrentFloor => _currentFloor;
        public IEnumerable<IFloor> Floors => _floors;
        public ILift Lift => _lift;

        public void ResetAllCommands()
        {
            _lift.ResetCurrentCommand();
        }
        
        private void LiftFloorChanged(int floor)
        {
            _currentFloor.Reset();
            _currentFloor = _floors[floor - 1];
            _currentFloor.UpdateState(_lift.State);
        }

        private void LiftStateChanged(LiftState state)
        {
            _currentFloor.UpdateState(state);
        }
        
        void ICommandReceiver.AddCommand(in LiftCommand command)
        {
            UnityEngine.Debug.Log($"Command: {command.Floor}, {command.Kind}");
        }

        private float debugTimer = 5f;

        public void Tick(float dt)
        {
            _lift.Tick(dt);
            if (debugTimer <= 0) return;
            debugTimer -= dt;
            if (debugTimer > 0) return;
            _lift.MoveTo(_floors[_floors.Length - 1].Number);
        }
        
    }

}