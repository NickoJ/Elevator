namespace Klyukay.Lift.Models
{
    
    public readonly struct LiftCommand
    {

        public readonly int Floor;
        public readonly MoveDirection Direction;

        public LiftCommand(int floor, MoveDirection direction)
        {
            Floor = floor;
            Direction = direction;
        }
        
    }

}