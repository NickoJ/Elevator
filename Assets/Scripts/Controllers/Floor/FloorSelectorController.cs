using System;
using Klyukay.Lift.Models;
using UnityEngine;

namespace Klyukay.Lift.Controllers
{
    
    public class FloorSelectorController : MonoBehaviour
    {

        [SerializeField] private FloorController floorPrefab;
        [SerializeField] private Transform root;

        public event Action<IFloor> OnFloorSelected; 
        
        public void Initialize(LiftManager manager)
        {
            foreach (var floor in manager.Floors)
            {
                var controller = Instantiate(floorPrefab, root);
                controller.Floor = floor;
            }

            OnFloorSelected?.Invoke(manager.CurrentFloor);
        }

        private void OnDestroy()
        {
            OnFloorSelected = null;
        }
    }
    
}