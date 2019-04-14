using System.Collections.Generic;
using UnityEngine;

namespace Klyukay.Lift.Models
{
    
    public class CommandAggregator
    {

        private readonly Dictionary<int, FloorState> _stateByFloor = new Dictionary<int,FloorState>();
        private readonly HashSet<FloorState> _activeFloorStates = new HashSet<FloorState>();
        
        private readonly List<FloorState> _selectionBuffer = new List<FloorState>();
        
        public bool HasCommand => _activeFloorStates.Count > 0;

        public int TakeNextFloor(MoveDirection currentDirection, int floor)
        {
            _selectionBuffer.Clear();
            _selectionBuffer.AddRange(_activeFloorStates);
            _selectionBuffer.Sort((x, y) => SortCommands(x, y, currentDirection, floor));

            var state = _selectionBuffer[0];
            
            state.MoveDirection = MoveDirection.Undefined;
            _activeFloorStates.Remove(state);
            
            return state.Floor;
        }

        public void AddCommand(in LiftCommand command)
        {
            if (!_stateByFloor.TryGetValue(command.Floor, out var state))
            {
                state = new FloorState(command.Floor);
                _stateByFloor[state.Floor] = state;
            }

            state.MoveDirection |= command.Direction;
            _activeFloorStates.Add(state);
        }

        public void Reset()
        {
            foreach (var state in _activeFloorStates)
            {
                state.MoveDirection = MoveDirection.Undefined;
            }
            
            _activeFloorStates.Clear();
            _selectionBuffer.Clear();
        }
        
        private static int SortCommands(FloorState x, FloorState y, MoveDirection direction, int floor)
        {
            var xValue = x.Floor - floor;
            var yValue = y.Floor - floor;

            if (xValue == 0) return -1;
            if (yValue == 0) return 1;

            if (xValue * yValue < 0)
            {
                return xValue * direction.ToInt() > 0 ? -1 : 1;
            }

            var result = Mathf.Abs(xValue) - Mathf.Abs(yValue);
            direction = xValue > 0 ? MoveDirection.Up : MoveDirection.Down;

            if (result < 0 && RequestToOtherSideOnly(x.MoveDirection, direction)) return 1;
            if (result > 0 && RequestToOtherSideOnly(y.MoveDirection, direction)) return -1;
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