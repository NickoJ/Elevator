using Klyukay.Lift.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Controllers
{

    public class LiftInsideController : MonoBehaviour
    {
     
        [SerializeField] private FloorController floorPrefab;

        [SerializeField] private Button resetButton;
        [SerializeField] private Transform root;
        
        public void Initialize(ILiftManager manager)
        {
            foreach (var floor in manager.Floors)
            {
                var controller = Instantiate(floorPrefab, root);
                controller.Floor = floor;
            }
            
            resetButton.transform.SetAsLastSibling();
            resetButton.onClick.AddListener(manager.ResetAllCommands);
        }
        
    }

}