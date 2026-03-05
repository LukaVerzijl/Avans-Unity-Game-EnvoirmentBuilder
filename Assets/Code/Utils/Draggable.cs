using System;
using Code.Managers;
using Code.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/*
* The GameObject also needs a collider otherwise OnMouseUpAsButton() can not be detected.
*/
public class Draggable: MonoBehaviour, IPointerClickHandler
{
    
        public bool isDragging = false;
        public string prefabId;
        public string id;
        public bool isNew = true;
    
        public void Update()
        {
            if (isDragging)
                this.transform.position = GetMousePosition();
        }
    
    
        private Vector3 GetMousePosition()
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(mousePos);
            positionInWorld.z = 0;
            return positionInWorld;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // if not dragging open ui
            if (!isDragging)
            {
                EditUI.Instance.ShowEditUI(this.gameObject);
                return;
            }
            isDragging = !isDragging;
    
            if (!isDragging)
            {
                EnvManager.Instance.SaveObject(id, prefabId, this.transform.position.x, this.transform.position.y, this.transform.rotation.eulerAngles.z, 1, isNew);
                UIManager.Instance.showSideUI();
                isNew = false;
            }
        }
}
