using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views
{
    
    [RequireComponent(typeof(Image))]
    public sealed class FloorSpriteIndicator : FloorIndicator
    {
     
        //TODO: To scriptable object
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite openedSprite;
        [SerializeField] private Sprite disabledSprite;
     
        private Image _image;

        protected override void Awake()
        {
            _image = GetComponent<Image>();
            base.Awake();
        }

        protected override void LaunchActiveState() => _image.sprite = activeSprite;
        protected override void LaunchOpenedState() => _image.sprite = openedSprite;
        protected override void LaunchDisabledState() => _image.sprite = disabledSprite;

        protected override void SetSwitchState(bool enabled) => _image.sprite = enabled ? activeSprite : disabledSprite;
        
    }
    
}