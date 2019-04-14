using System.Collections.Generic;

namespace Klyukay.Lift.Models
{
    
    public interface ILiftManager
    {

        IFloor CurrentFloor { get; }
        IEnumerable<IFloor> Floors { get; }

        void ResetAllCommands();
        
    }
    
}