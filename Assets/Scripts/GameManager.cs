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

        private void Start()
        {
            enabled = false;
        }

        public void Initialize(int floorsCount, ILiftSettings liftSettings)
        {
            _manager = new LiftManager(floorsCount, liftSettings);

            floorSelector.Initialize(_manager);
            insideController.Initialize(_manager);
            liftController.Lift = _manager.Lift;
            
            enabled = true;
        }

        private void Update()
        {
            _manager.Tick(Time.deltaTime);
        }

    }
    
}