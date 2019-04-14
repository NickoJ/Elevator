using System;
using Klyukay.Lift.Controllers;
using Klyukay.Lift.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Klyukay.Lift.Views.Floor
{

    [RequireComponent(typeof(FloorController))]
    public class FloorSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
        IPointerClickHandler
    {

        [SerializeField] private Image image;

        [SerializeField] private Sprite focusSprite;
        [SerializeField] private Sprite selectedSprite;
        [SerializeField] private Sprite notSelectedSprite;

        private FloorController floorController;
        
        private bool _focused;
        private bool _selected;

        public bool Focused
        {
            get => _focused;
            private set
            {
                _focused = value;
                UpdateView();
            }
        }

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                UpdateView();
            }
        }

        public IFloor Floor => floorController.Floor;

        public event Action<FloorSelection> OnFloorFocusStateChanged;

        private void Awake()
        {
            floorController = GetComponent<FloorController>();
        }

        private void OnDestroy()
        {
            OnFloorFocusStateChanged = null;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            Focused = true;
            OnFloorFocusStateChanged?.Invoke(this);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            Focused = false;
            OnFloorFocusStateChanged?.Invoke(this);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Selected = true;
            OnFloorFocusStateChanged?.Invoke(this);
        }

        private void UpdateView()
        {
            image.sprite = Selected ? selectedSprite : Focused ? focusSprite : notSelectedSprite;
        }

    }

}