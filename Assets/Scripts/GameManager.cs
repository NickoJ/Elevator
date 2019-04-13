using Klyukay.Lift.Controllers;
using Klyukay.Lift.Models;
using UnityEngine;

namespace Klyukay.Lift
{
    
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private FloorSelectorController floorSelector;
        [SerializeField] private LiftController liftController;

        private ITickable _tickable;
        
        private void Awake()
        {
            //TODO: 10 to configurations
            var manager = new LiftManager(10);
            _tickable = manager;
            
            floorSelector.Initialize(manager);
            liftController.Lift = manager.Lift;
        }

        private void Update() => _tickable.Tick(Time.deltaTime);

    }
    
}