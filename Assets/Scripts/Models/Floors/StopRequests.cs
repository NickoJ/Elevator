namespace Klyukay.Lift.Models
{
    
    [System.Flags]
    public enum StopRequests
    {
        NoRequests = 0b0,
        MoveUp = 0b1,
        MoveDown = 0b10,
        Exit = 0b100
    }

}