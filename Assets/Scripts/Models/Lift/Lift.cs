using System;
using UnityEngine;

namespace Klyukay.Lift.Models
{
    
    public class Lift : ILift, ITickable
    {
        
        //TODO: All to settings
        private const float MOVE_TIME = 3f;
        private const float OPENING_TIME = 4f;
        private const float CLOSING_TIME = 4f;
        private const float OPENED_TIME = 10f;

        private int _currentFloor;
        private int _moveToFloor;
        private LiftState _state;

        private float _timer;
        
        public Lift(int currentCurrentFloor)
        {
            CurrentFloor = currentCurrentFloor;
            _moveToFloor = CurrentFloor;
            _state = LiftState.Closed;
        }

        public int CurrentFloor
        {
            get => _currentFloor;
            private set
            {
                _currentFloor = value;
                OnFloorChanged?.Invoke(_currentFloor);   
            }
        }

        public LiftState State
        {
            get => _state;
            private set
            {
                _state = value;
                OnStateChanged?.Invoke(_state);
            }
        }

        public MoveDirection Direction
        {
            get
            {
                var diff = _moveToFloor - _currentFloor;
                if (diff < 0) return MoveDirection.Down;
                if (diff > 0) return MoveDirection.Up;
                return MoveDirection.NoDirection;
            }
        }

        public bool Active => State != LiftState.Closed; 
        
        public event Action<int> OnFloorChanged;
        public event Action<LiftState> OnStateChanged; 

        public void MoveTo(int number)
        {
            if (State != LiftState.Closed)
            {
                Debug.LogError("Try to move lift in move");
                return;
            }

            _moveToFloor = number;
            _timer = MOVE_TIME;
            State = LiftState.Moving;
        }
        
        public void Tick(float dt)
        {
            if (!Active) return;
            _timer -= dt;
            if (_timer <= 0) Act();
        }

        private void Act()
        {
            switch (State)
            {
                case LiftState.Moving: MoveAct(); break;
                case LiftState.Opening: OpeningAct(); break;     
                case LiftState.Opened: OpenedAct(); break;
                case LiftState.Closing: ClosingAct(); break;
                case LiftState.Closed: break;
            }
        }

        private void MoveAct()
        {
            CurrentFloor = _currentFloor + Direction.ToInt();
            if (CurrentFloor != _moveToFloor)
            {
                _timer = MOVE_TIME;
            }
            else
            {
                State = LiftState.Opening;
                _timer = OPENING_TIME;
            }
        }

        private void OpeningAct()
        {
            State = LiftState.Opened;
            _timer = OPENED_TIME;
        }

        private void OpenedAct()
        {
            State = LiftState.Closing;
            _timer = CLOSING_TIME;
        }

        private void ClosingAct()
        {
            State = LiftState.Closed;
        }
        
    }
    
}