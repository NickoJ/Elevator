using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Lift.Views
{

    [RequireComponent(typeof(Slider))]
    public class SliderWithValues : MonoBehaviour
    {

        [SerializeField] private Text minValueText;
        [SerializeField] private Text maxValueText;
        [SerializeField] private Text currentValueText;

        private bool _initialized;
        private float _lastMinValue;
        private float _lastMaxValue;
        private float _lastCurrentValue;
        
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnChange);
            OnChange(0.0f);
            _initialized = true;
        }

        private void OnChange(float _)
        {
            _lastMinValue = ChangeText(minValueText, _lastMinValue, _slider.minValue);
            _lastMaxValue = ChangeText(maxValueText, _lastMaxValue, _slider.maxValue);
            _lastCurrentValue = ChangeText(currentValueText, _lastCurrentValue, _slider.value);
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private float ChangeText(Text text, float lastValue, float value)
        {
            if (_initialized && lastValue == value) return lastValue;
            text.text = value.ToString(CultureInfo.CurrentCulture);
            return value;
        }

    }

}