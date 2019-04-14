using Klyukay.Lift.Models;
using UnityEngine;

namespace Klyukay.Lift.Settings
{
    
    [CreateAssetMenu(menuName = "Elevator/Lift Settings", fileName = "LiftSettings")]
    public class LiftSettings : ScriptableObject, ILiftSettings
    {

        [SerializeField] private int minValue;
        [SerializeField] private int maxValue;
        [SerializeField] private int startValue;
        [SerializeField] private int moveTime;
        [SerializeField] private int openingTime;
        [SerializeField] private int closingTime;
        [SerializeField] private int openedTime;

        public int MinValue => minValue > 0 ? minValue : 1;
        public int MaxValue => maxValue > MinValue ? maxValue : MinValue + 1;
        public int StartValue => Mathf.Clamp(startValue, MinValue, MaxValue);

        public float MoveTime => moveTime > 0f ? moveTime : 1f;
        public float OpeningTime => openingTime > 0f ? openingTime : 1f;
        public float ClosingTime => closingTime > 0f ? closingTime : 1f;
        public float OpenedTime => openedTime > 0f ? openedTime : 1f;

    }
    
}