using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klyukay.Lift.Models
{
    
    public class CommandAggregator
    {

        private readonly Dictionary<int, FloorState> _stateByFloor = new Dictionary<int,FloorState>();
        private readonly HashSet<FloorState> _activeFloorStates = new HashSet<FloorState>();
        
        private readonly List<FloorState> _selectionBuffer = new List<FloorState>();

        private LiftCommand? _lastCommand;
        
        public bool HasCommand => _activeFloorStates.Count > 0;

        public event Action OnInterruption; 
        
        public int TakeNextFloor(MoveDirection currentDirection, int floor)
        {
            _selectionBuffer.Clear();
            _selectionBuffer.AddRange(_activeFloorStates);
            _selectionBuffer.Sort((x, y) => CompareStates(x, y, floor, currentDirection));

            var state = _selectionBuffer[0];
            
            _lastCommand = new LiftCommand(state.Floor, state.MoveDirection);
            
            state.MoveDirection = MoveDirection.Undefined;
            _activeFloorStates.Remove(state);
            
            return state.Floor;
        }

        public void AddCommand(in LiftCommand command, int currentFloor, MoveDirection currentDir)
        {
            AddCommand(command);

            if (_lastCommand != null)
            {
                var lc = _lastCommand.Value;
                var compareRes = CompareFloors(command.Floor, command.Direction, lc.Floor, lc.Direction, 
                    currentFloor,currentDir);

                if (compareRes < 0)
                {
                    _lastCommand = null;
                    AddCommand(lc);
                    OnInterruption?.Invoke();
                }
            }
        }

        private void AddCommand(in LiftCommand command)
        {
            if (!_stateByFloor.TryGetValue(command.Floor, out var state))
            {
                state = new FloorState(command.Floor);
                _stateByFloor[state.Floor] = state;
            }

            state.MoveDirection |= command.Direction;
            _activeFloorStates.Add(state);
        }
        
        public void ForgetLastCommand() => _lastCommand = null;

        public void Reset()
        {
            foreach (var state in _activeFloorStates)
            {
                state.MoveDirection = MoveDirection.Undefined;
            }
            
            _activeFloorStates.Clear();
            _selectionBuffer.Clear();
            _lastCommand = null;
        }
        
        private static int CompareStates(FloorState x, FloorState y, int floor, MoveDirection direction)
        {
            return CompareFloors(x.Floor, x.MoveDirection, y.Floor, y.MoveDirection, floor, direction);
        }

        private static int CompareFloors(int xFloor, MoveDirection xDirection, int yFloor, MoveDirection yDirection,
            int floor, MoveDirection direction)
        {
            var xValue = xFloor - floor;
            var yValue = yFloor - floor;

            if (xValue == 0) return -1;
            if (yValue == 0) return 1;

            if (xValue * yValue < 0)
            {
                return xValue * direction.ToInt() > 0 ? -1 : 1;
            }

            var result = Mathf.Abs(xValue) - Mathf.Abs(yValue);
            direction = xValue > 0 ? MoveDirection.Up : MoveDirection.Down;

            if (result < 0 && RequestToOtherSideOnly(xDirection, direction)) return 1;
            if (result > 0 && RequestToOtherSideOnly(yDirection, direction)) return -1;
            return result;
        }

        private static bool RequestToOtherSideOnly(MoveDirection requestDir, MoveDirection currentDir)
        {
            return (requestDir & MoveDirection.NoDirection) == MoveDirection.Undefined &&
                   (requestDir & currentDir) == MoveDirection.Undefined &&
                   (requestDir & currentDir.Invert()) != MoveDirection.Undefined;
        }

        private class FloorState
        {

            public readonly int Floor;

            public FloorState(int floor)
            {
                Floor = floor;
            }

            public MoveDirection MoveDirection { get; set; }

        }
    }
    
}