using Klyukay.Lift.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Lift
{
    
    [RequireComponent(typeof(Text))]
    public sealed class LiftCurrentState : LiftView
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
            Lift.OnStateChanged += OnStateChanged;
        }

        protected override void DeleteConnections(ILift lift)
        {
            base.DeleteConnections(lift);
            Lift.OnStateChanged -= OnStateChanged;
        }

        protected override void UpdateView()
        {
            if (Lift == null) return;
            OnStateChanged(Lift.State);  
        }

        private void OnStateChanged(LiftState state) => _text.text = state.ToString();

    }
}