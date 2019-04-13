using System;

namespace Klyukay.Lift.Models
{
    
    public class Floor : IFloor
    {

        private readonly ICommandReceiver _receiver;
        private LiftState? _liftState;

        public Floor(int number, ICommandReceiver receiver)
        {
            Number = number;
            _receiver = receiver;
        }

        public int Number { get; }
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

        public event Action<LiftState?> OnStateChanged; 

        public void UpdateState(LiftState state) => LiftState = state;
        public void Reset() => LiftState = null;
        
        public void MoveUp() => SendCommand(new LiftCommand(Number, CommandKind.MoveUp));
        public void MoveDown() => SendCommand(new LiftCommand(Number, CommandKind.MoveDown));

        private void SendCommand(in LiftCommand command) => _receiver?.AddCommand(command);

    }
    
}