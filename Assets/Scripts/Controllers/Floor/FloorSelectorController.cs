using System;
using Klyukay.Lift.Models;
using Klyukay.Lift.Views.Floor;
using UnityEngine;

namespace Klyukay.Lift.Controllers
{
    
    public class FloorSelectorController : MonoBehaviour
    {

        [SerializeField] private FloorController floorPrefab;
        [SerializeField] private Transform root;

        private FloorSelection _selected;
        private FloorSelection _focused;

        public event Action<IFloor> OnFloorSelected; 
        
        public void Initialize(LiftManager manager)
        {
            foreach (var floor in manager.Floors)
            {
                var controller = Instantiate(floorPrefab, root);
                controller.Floor = floor;
                
                var selection = controller.GetComponent<FloorSelection>();
                if (selection != null)
                {
                    selection.OnFloorFocusStateChanged += FloorFocusStateChanged;
                    if (selection.Floor == manager.CurrentFloor)
                    {
                        _selected = selection;
                        _selected.Selected = true;
                    }
                }
            }

            OnFloorSelected?.Invoke(_selected != null ? _selected.Floor : manager.CurrentFloor);
        }

        private void OnDestroy()
        {
            OnFloorSelected = null;
        }

        private void FloorFocusStateChanged(FloorSelection selection)
        {
            if (selection.Selected && _selected != selection)
            {
                _selected.Selected = false;
                _selected = selection;
            }
            
            if (selection.Focused) _focused = selection;
            else if (_focused == selection) _focused = null;
            
            OnFloorSelected?.Invoke(_focused != null ? _focused.Floor : _selected.Floor);
        }
        
    }
    
}