namespace Klyukay.Lift.Views.Floor
{
    
    public sealed class FloorButtonUp : FloorButton
    {
        
        protected override void OnClick() => Floor.MoveUp();
        
    }
    
}