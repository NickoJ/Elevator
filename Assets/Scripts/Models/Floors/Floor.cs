using System;

namespace Klyukay.Lift.Models
{
    
    public class Floor : IFloor
    {

        private readonly ICommandReceiver _receiver;
        private LiftState? _liftState;
        private StopRequests _stopRequests;

        public Floor(int number, ICommandReceiver receiver, bool canMoveUp, bool canMoveDown)
        {
            Number = number;
            _receiver = receiver;
            CanMoveUp = canMoveUp;
            CanMoveDown = canMoveDown;
        }

        public int Number { get; }
        public bool CanMoveUp { get; }
        public bool CanMoveDown { get; }

        public LiftState? LiftState
        {
            get => _liftState;
            private set
            {
                if (_liftState == value) return;
                _liftState = value;
                OnStateChanged?.Invoke(_liftState);
            }
        }

        public StopRequests StopRequests
        {
            get => _stopRequests;
            private set
            {
                if (_stopRequests == value) return;
                _stopRequests = value;
                OnStopRequestsChanged?.Invoke(_stopRequests);
            }
        }

        public event Action<LiftState?> OnStateChanged;
        public event Action<StopRequests> OnStopRequestsChanged;

        public void UpdateState(LiftState state) => LiftState = state;
        public void Reset() => LiftState = null;

        public void MoveUp()
        {
            StopRequests |= StopRequests.MoveUp;
            SendCommand(new LiftCommand(Number, CommandKind.MoveUp));
        }

        public void MoveDown()
        {
            StopRequests |= StopRequests.MoveDown;
            SendCommand(new LiftCommand(Number, CommandKind.MoveDown));
        }

        public void Exit()
        {
            StopRequests |= StopRequests.Exit;
            SendCommand(new LiftCommand(Number, CommandKind.Exit));
        }
        
        private void SendCommand(in LiftCommand command) => _receiver?.AddCommand(command);

    }
    
}