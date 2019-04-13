using System;

namespace Klyukay.Lift.Models
{
    
    public interface ILift
    {
        int CurrentFloor { get; }
        LiftState State { get; }

        event Action<int> OnFloorChanged;
        event Action<LiftState> OnStateChanged;

    }
    
}