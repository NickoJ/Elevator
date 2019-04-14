using System;

namespace Klyukay.Lift.Models
{
    public interface IFloor
    {
        int Number { get; }
        LiftState? LiftState { get; }
        
        StopRequests StopRequests { get; }

        event Action<LiftState?> OnStateChanged;
        event Action<StopRequests> OnStopRequestsChanged;
        
        void MoveUp();
        void MoveDown();
        void Exit();
        
    }
}