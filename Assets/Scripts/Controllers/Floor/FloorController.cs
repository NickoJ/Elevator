using System;
using Klyukay.Lift.Models;
using UnityEngine;

namespace Klyukay.Lift.Controllers
{
    
    public class FloorController : MonoBehaviour
    {

        private IFloor _floor;

        public IFloor Floor
        {
            get => _floor;
            set
            {
                _floor = value;
                OnFloorChanged?.Invoke(_floor);
            }
        }

        public event Action<IFloor> OnFloorChanged;

        private void OnDestroy()
        {
            OnFloorChanged = null;
        }
        
    }
    
}