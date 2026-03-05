using System;
using Code.Managers;
using Code.Utils;
using UnityEngine;

namespace Code.UI
{
    public class EditUI : Singleton<EditUI>
    {
        public string objectId;
        public void onEditButtonClick()
        {
            GameObject gameObject =
                EnvManager.Instance.loadedObjects.Find(o => o.GetComponent<Draggable>().id == objectId);

            gameObject.GetComponent<Draggable>().isDragging = true;
            this.gameObject.SetActive(false);
        }

        public void onDeleteButtonClick()
        {
            EnvManager.Instance.DeleteObject(objectId);
            this.gameObject.SetActive(false);
            
        }
        
        public void ShowEditUI(GameObject go)
        {
                objectId = go.GetComponent<Draggable>().id;
                // this.gameObject.transform.position = new Vector3(0, 0, 0);
                this.gameObject.SetActive(true);
        }

        public void Start()
        {
            this.gameObject.SetActive(false);
        }
    }
}