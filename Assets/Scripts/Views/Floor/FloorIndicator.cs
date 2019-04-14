using System.Collections;
using Klyukay.Lift.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Floor
{

    public abstract class FloorIndicator : FloorView
    {

        [SerializeField] private float switchTime = 0.2f;

        protected override void SetupConnections(IFloor floor)
        {
            base.SetupConnections(floor);
            floor.OnStateChanged += FloorStateChanged;
        }

        protected override void DeleteConnections(IFloor floor)
        {
            base.DeleteConnections(floor);
            floor.OnStateChanged -= FloorStateChanged;
        }

        protected override void UpdateView() => FloorStateChanged(Floor.LiftState);

        private void FloorStateChanged(LiftState? inState)
        {
            StopAllCoroutines();

            switch (inState)
            {
                case null: LaunchDisabledState(); break;
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

        protected abstract void LaunchActiveState();
        private void LaunchSwitchingState() => StartCoroutine(SwitchingRoutine());
        protected abstract void LaunchOpenedState();
        protected abstract void LaunchDisabledState();

        protected abstract void SetSwitchState(bool enabled);
        
        private IEnumerator SwitchingRoutine()
        {
            var switchWait = new WaitForSeconds(switchTime);
            var nextState = false;
            while (true)
            {
                SetSwitchState(nextState);
                nextState = !nextState;
                yield return switchWait;
            }
        }
        
    }
    
}