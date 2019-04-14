using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Floor
{

    [RequireComponent(typeof(Text))]
    public sealed class FloorNumber : FloorView
    {

        private Text _text;

        protected override void Awake()
        {
            _text = GetComponent<Text>();
            base.Awake();
        }

        protected override void UpdateView()
        {
            _text.text = Floor?.Number.ToString();
        }
        
    }
    
}