using System;

namespace Klyukay.Lift.Models
{
    public interface IFloor
    {
        int Number { get; }
        LiftState? LiftState { get; }

        event Action<LiftState?> OnStateChanged; 
        
        void MoveUp();
        void MoveDown();
    }
}