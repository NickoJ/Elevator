using Klyukay.Lift.Controllers;
using Klyukay.Lift.Models;
using UnityEngine;

namespace Klyukay.Lift.Views.Lift
{
    
    public abstract class LiftView : MonoBehaviour
    {
        
        [SerializeField] private LiftController controller;

        protected ILift Lift { get; private set; }

        protected virtual void Awake()
        {
            controller.OnLiftChanged += ChangeLift;
        }

        private void OnDestroy()
        {
            controller.OnLiftChanged -= ChangeLift;
            if (Lift != null) DeleteConnections(Lift);
        }

        private void ChangeLift(ILift lift)
        {
            if (Lift != null) DeleteConnections(Lift);
            Lift = lift;
            if (Lift != null) SetupConnections(Lift);
            UpdateView();
        }

        protected virtual void SetupConnections(ILift lift) {}
        protected virtual void DeleteConnections(ILift lift) {}

        protected abstract void UpdateView();
        
    }
    
}