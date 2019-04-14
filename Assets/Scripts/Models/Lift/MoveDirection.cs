namespace Klyukay.Lift.Models
{
    
    [System.Flags]
    public enum MoveDirection : byte
    {
        Undefined = 0b0,
        NoDirection = 0b1,
        Up = 0b10,
        Down = 0b100
    }

    public static class MoveDirectionUtils
    {

        public static int ToInt(this MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up: return 1;
                case MoveDirection.Down: return -1;
                default: return 0;
            }
        }

        public static MoveDirection Invert(this MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up: return MoveDirection.Down;
                case MoveDirection.Down: return MoveDirection.Up;
                default: return MoveDirection.Undefined;
            }
        }

    }
    
}