namespace Klyukay.Lift.Views.Floor
{
    
    public abstract class FloorButtonShowView : FloorView
    {

        protected abstract bool CanShowButton { get; }

        protected override void UpdateView() => gameObject.SetActive(CanShowButton);
        
    }
    
}