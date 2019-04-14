using Klyukay.Lift.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Floor
{
    
    public sealed class FloorStopRequestsIndicator : FloorView
    {
        
        [SerializeField] private Color noRequestColor;
        [SerializeField] private Color requestColor;
        [SerializeField] private StopRequests request;

        [SerializeField] private Graphic graphic;
        
        protected override void SetupConnections(IFloor floor)
        {
            base.SetupConnections(floor);
            floor.OnStopRequestsChanged += StopRequestsChanged;
        }

        protected override void DeleteConnections(IFloor floor)
        {
            base.DeleteConnections(floor);
            floor.OnStopRequestsChanged -= StopRequestsChanged;
        }

        protected override void UpdateView() => StopRequestsChanged(Floor?.StopRequests ?? StopRequests.NoRequests);

        private void StopRequestsChanged(StopRequests inRequests)
        {
            graphic.color = (inRequests & request) != 0 ? requestColor : noRequestColor;
        }
        
    }
    
}