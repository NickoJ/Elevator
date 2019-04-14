using Klyukay.Lift.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Controllers
{
    
    public class LiftSettingsController : MonoBehaviour
    {

        [SerializeField] private LiftSettings settings;

        [SerializeField] private GameManager gameManager;
        
        [SerializeField] private Button okButton;
        [SerializeField] private Slider slider;
        
        private void Awake()
        {
            slider.wholeNumbers = true;
            slider.minValue = settings.MinValue;
            slider.maxValue = settings.MaxValue;
            slider.value = settings.StartValue;
            
            okButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            gameManager.Initialize((int)slider.value, settings);
            Destroy(gameObject);
        }
        
    }
    
}