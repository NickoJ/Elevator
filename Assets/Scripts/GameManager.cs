using Klyukay.Lift.Controllers;
using Klyukay.Lift.Models;
using UnityEngine;

namespace Klyukay.Lift
{
    
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private FloorSelectorController floorSelector;
        [SerializeField] private LiftController liftController;
        [SerializeField] private LiftInsideController insideController;

        private LiftManager _manager;
        
        private void Awake()
        {
            //TODO: 10 to configurations
            _manager = new LiftManager(10);
            
            floorSelector.Initialize(_manager);
            insideController.Initialize(_manager);
            liftController.Lift = _manager.Lift;
        }

        private void Update() => _manager.Tick(Time.deltaTime);

    }
    
}