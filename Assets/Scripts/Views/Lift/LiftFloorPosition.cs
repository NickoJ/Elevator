using Klyukay.Lift.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Lift
{
    
    [RequireComponent(typeof(Text))]
    public sealed class LiftFloorPosition : LiftView
    {

        private Text _text;

        protected override void Awake()
        {
            _text = GetComponent<Text>();
            base.Awake();
        }

        protected override void SetupConnections(ILift lift)
        {
            base.SetupConnections(lift);
            Lift.OnFloorChanged += OnFloorChanged;
        }

        protected override void DeleteConnections(ILift lift)
        {
            base.DeleteConnections(lift);
            Lift.OnFloorChanged -= OnFloorChanged;
        }

        protected override void UpdateView()
        {
            if (Lift == null) return;
            OnFloorChanged(Lift.CurrentFloor);  
        }

        private void OnFloorChanged(int floor) => _text.text = floor.ToString();

    }
}