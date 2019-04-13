using Klyukay.Lift.Controllers;
using Klyukay.Lift.Models;
using UnityEngine;

namespace Klyukay.Lift.Views
{
    
    public abstract class FloorView : MonoBehaviour
    {

        [SerializeField] private FloorController controller;

        protected IFloor Floor { get; private set; }

        protected virtual void Awake()
        {
            controller.OnFloorChanged += ChangeFloor;
        }

        private void OnDestroy()
        {
            controller.OnFloorChanged -= ChangeFloor;
            if (Floor != null) DeleteConnections(Floor);
        }

        private void ChangeFloor(IFloor floor)
        {
            if (Floor != null) DeleteConnections(Floor);
            Floor = floor;
            if (Floor != null) SetupConnections(Floor);
            UpdateView();
        }

        protected virtual void SetupConnections(IFloor floor) {}
        protected virtual void DeleteConnections(IFloor floor) {}

        protected abstract void UpdateView();

    }
    
}