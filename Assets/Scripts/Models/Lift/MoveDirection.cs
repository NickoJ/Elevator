namespace Klyukay.Lift.Models
{
    
    public enum MoveDirection : byte
    {
        NoDirection,
        Up,
        Down
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
        
    }
    
}