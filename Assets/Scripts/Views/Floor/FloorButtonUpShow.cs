namespace Klyukay.Lift.Views.Floor
{
    public sealed class FloorButtonUpShow : FloorButtonShowView
    {
        
        protected override bool CanShowButton => Floor?.CanMoveUp ?? false;
        
    }
}