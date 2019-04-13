using System.Collections;
using Klyukay.Lift.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Lift
{

    public sealed class LiftColorIndicator : LiftView
    {

        [SerializeField] private float switchTime = 0.2f;
        [SerializeField] private Graphic[] graphics;
        
        //TODO: To scriptable object
        [SerializeField] private Color activeColor;
        [SerializeField] private Color openedColor;
        [SerializeField] private Color switchColor;

        protected override void SetupConnections(ILift lift)
        {
            base.SetupConnections(lift);
            lift.OnStateChanged += LiftStateChanged;
        }

        protected override void DeleteConnections(ILift lift)
        {
            base.DeleteConnections(lift);
            lift.OnStateChanged -= LiftStateChanged;
        }

        protected override void UpdateView() => LiftStateChanged(Lift.State);

        private void LiftStateChanged(LiftState inState)
        {
            StopAllCoroutines();

            switch (inState)
            {
                case LiftState.Moving: 
                case LiftState.Closed:
                    LaunchActiveState(); 
                    break;
                case LiftState.Opening:
                case LiftState.Closing:
                    LaunchSwitchingState();
                    break;
                case LiftState.Opened:
                    LaunchOpenedState();
                    break;
            }
        }

        private void LaunchActiveState() => SetColor(activeColor);
        private void LaunchSwitchingState() => StartCoroutine(SwitchingRoutine());
        private void LaunchOpenedState() => SetColor(openedColor);
        
        private IEnumerator SwitchingRoutine()
        {
            var switchWait = new WaitForSeconds(switchTime);
            var nextColor = switchColor;
            while (true)
            {
                SetColor(nextColor);
                nextColor = nextColor == switchColor ? activeColor : switchColor;
                yield return switchWait;
            }
        }
        
        private void SetColor(Color color)
        {
            foreach (var g in graphics) g.color = color;
        }
        
    }

}