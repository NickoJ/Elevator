using System;
using Klyukay.Lift.Models;
using UnityEngine;

namespace Klyukay.Lift.Controllers
{
    
    public class LiftController : MonoBehaviour
    {
     
        private ILift _lift;

        public ILift Lift
        {
            set
            {
                _lift = value;
                OnLiftChanged?.Invoke(_lift);
            }
        }

        public event Action<ILift> OnLiftChanged;

        private void OnDestroy()
        {
            OnLiftChanged = null;
        }
        
    }
    
}