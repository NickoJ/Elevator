using Klyukay.Lift.Models;
using UnityEngine;

namespace Klyukay.Lift.Controllers
{
    
    public class FloorOutsideController : MonoBehaviour
    {

        [SerializeField] private FloorSelectorController selector;
        [SerializeField] private FloorController floorController;

        private void Awake()
        {
            selector.OnFloorSelected += OnFloorSelected;
        }

        private void OnDestroy()
        {
            selector.OnFloorSelected -= OnFloorSelected;
        }

        private void OnFloorSelected(IFloor floor) => floorController.Floor = floor;
        
    }
    
}