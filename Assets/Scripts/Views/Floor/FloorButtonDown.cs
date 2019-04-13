namespace Klyukay.Lift.Views
{
    public sealed class FloorButtonDown : FloorButton
    {
        protected override void OnClick() => Floor.MoveDown();
    }
}