using System;
using System.Collections.Generic;

namespace Klyukay.Lift.Models
{

    public class LiftManager : ILiftManager, ICommandReceiver, ITickable
    {

        private readonly Lift _lift;
        private readonly CommandAggregator _aggregator;
        private readonly Floor[] _floors;
        private Floor _currentFloor;

        private MoveDirection _lastDirection;
        
        public LiftManager(int floorsCount)
        {
            _floors = new Floor[floorsCount];
            
            for (int i = 0; i < floorsCount; i++)
            {
                _floors[i] = new Floor(i + 1, this, i + 1 != floorsCount, i != 0);
            }

            _currentFloor = _floors[0];
            _lift = new Lift(_currentFloor.Number);
            _lift.OnFloorChanged += LiftFloorChanged;
            _lift.OnStateChanged += LiftStateChanged;

            _aggregator = new CommandAggregator();
            _aggregator.OnInterruption += OnAggregatorInterrupt;

            LiftStateChanged(_lift.State);
        }

        private void OnAggregatorInterrupt() => _lift.ResetCurrentCommand();

        public IFloor CurrentFloor => _currentFloor;
        public IEnumerable<IFloor> Floors => _floors;
        public ILift Lift => _lift;

        public void ResetAllCommands()
        {
            _aggregator.Reset();
            _lift.ResetCurrentCommand();
        }
        
        void ICommandReceiver.AddCommand(in LiftCommand command)
        {
            _aggregator.AddCommand(command, _lift.CurrentFloor, _lift.Direction);
            TryToSendNextCommand();
        }
        
        private void LiftFloorChanged(int floor)
        {
            _currentFloor.ResetState();
            _currentFloor = _floors[floor - 1];
            _currentFloor.UpdateState(_lift.State);
        }

        private void LiftStateChanged(LiftState state)
        {
            _currentFloor.UpdateState(state);
            if (state == LiftState.Closed) _aggregator.ForgetLastCommand();
        }

        private void TryToSendNextCommand()
        {
            if (_lift.State != LiftState.Closed || !_aggregator.HasCommand) return;
            
            var floor = _aggregator.TakeNextFloor(_lastDirection, _currentFloor.Number);
            _lift.MoveTo(floor);
            _lastDirection = _lift.Direction;
        }

        public void Tick(float dt)
        {
            TryToSendNextCommand();
            _lift.Tick(dt);
        }

    }

}