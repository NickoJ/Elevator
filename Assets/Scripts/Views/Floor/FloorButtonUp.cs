namespace Klyukay.Lift.Views
{
    
    public sealed class FloorButtonUp : FloorButton
    {
        
        protected override void OnClick() => Floor.MoveUp();
        
    }
    
}