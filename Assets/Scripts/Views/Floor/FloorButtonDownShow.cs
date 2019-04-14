namespace Klyukay.Lift.Views.Floor
{
    public sealed class FloorButtonDownShow : FloorButtonShowView
    {

        protected override bool CanShowButton => Floor?.CanMoveDown ?? false;

    }
}