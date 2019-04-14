using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Floor
{
    
    [RequireComponent(typeof(Button))]
    public abstract class FloorButton : FloorView
    {

        protected override void Awake()
        {
            base.Awake();
            var button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }
        
        protected override void UpdateView()
        {
        }

        protected abstract void OnClick();

    }
    
}