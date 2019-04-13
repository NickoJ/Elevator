using Klyukay.Lift.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Lift
{
    [RequireComponent(typeof(Text))]
    public sealed class LiftDirection : LiftView
    {
        //TODO: To scriptable object
        [SerializeField] private string moveUp = "U";
        [SerializeField] private string moveDown = "D";
        [SerializeField] private string stay = "-";

        private Text _text;

        protected override void Awake()
        {
            _text = GetComponent<Text>();
            base.Awake();
        }

        protected override void SetupConnections(ILift lift)
        {
            base.SetupConnections(lift);
            lift.OnStateChanged += OnStateChanged;
        }

        protected override void DeleteConnections(ILift lift)
        {
            base.DeleteConnections(lift);
            lift.OnStateChanged -= OnStateChanged;
        }

        protected override void UpdateView()
        {
            if (Lift != null) OnStateChanged(Lift.State);
        }

        private void OnStateChanged(LiftState _)
        {
            string str;
            switch (Lift.Direction)
            {
                case MoveDirection.Up:
                    str = moveUp;
                    break;
                case MoveDirection.Down:
                    str = moveDown;
                    break;
                default:
                    str = stay;
                    break;
            }

            _text.text = str;
        }
    }
}