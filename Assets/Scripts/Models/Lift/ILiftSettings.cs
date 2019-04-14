namespace Klyukay.Lift.Models
{
    
    public interface ILiftSettings
    {
        
        float MoveTime { get; }
        float OpeningTime { get; }
        float ClosingTime { get; }
        float OpenedTime { get; }
        
    }
    
}