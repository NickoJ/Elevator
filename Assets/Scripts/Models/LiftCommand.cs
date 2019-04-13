namespace Klyukay.Lift.Models
{
    
    public readonly struct LiftCommand
    {

        public readonly int Floor;
        public readonly CommandKind Kind;

        public LiftCommand(int floor, CommandKind kind)
        {
            Floor = floor;
            Kind = kind;
        }
        
    }

    public enum CommandKind : byte
    {
        MoveUp,
        MoveDown,
        Stop
    }
    
}