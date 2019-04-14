using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Floor
{
    
    public sealed class FloorColorIndicator : FloorIndicator
    {

        [SerializeField] private Graphic[] graphics;
        
        [SerializeField] private Color activeColor;
        [SerializeField] private Color openedColor;
        [SerializeField] private Color disabledColor;

        protected override void LaunchActiveState() => SetColor(activeColor);
        protected override void LaunchOpenedState() => SetColor(openedColor);
        protected override void LaunchDisabledState() => SetColor(disabledColor);

        protected override void SetSwitchState(bool enabled) => SetColor(enabled ? activeColor : disabledColor);

        private void SetColor(Color color)
        {
            foreach (var g in graphics) g.color = color;
        }
        
    }
    
}