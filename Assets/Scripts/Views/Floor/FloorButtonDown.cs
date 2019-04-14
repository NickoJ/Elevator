namespace Klyukay.Lift.Views.Floor
{
    public sealed class FloorButtonDown : FloorButton
    {
        protected override void OnClick() => Floor.MoveDown();
    }
}