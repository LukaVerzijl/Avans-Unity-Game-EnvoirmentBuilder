using System;
using Code.Managers;
using UnityEngine;

/*
* The GameObject also needs a collider otherwise OnMouseUpAsButton() can not be detected.
*/
public class Draggable: MonoBehaviour
{
    
        public bool isDragging = false;
        public string prefabId;
        public string id;
        private bool isNew = true;
    
        public void Update()
        {
            if (isDragging)
                this.transform.position = GetMousePosition();
        }
    
        private void OnMouseUpAsButton()
        {
            isDragging = !isDragging;
    
            if (!isDragging)
            {
                EnvManager.Instance.SaveObject(id, prefabId, this.transform.position.x, this.transform.position.y, this.transform.rotation.eulerAngles.z, this.GetComponent<SpriteRenderer>().sortingOrder, isNew);
                UIManager.Instance.showSideUI();
                isNew = false;
            }
        }
    
        private Vector3 GetMousePosition()
        {
            Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionInWorld.z = 0;
            return positionInWorld;
        }
        
}
