using System.Collections.Generic;
using Code.Utils;
using UnityEngine;

namespace Code.Managers
{
    public class EnvManager : Singleton<EnvManager>
    {
        public List<GameObject> objects = new List<GameObject>();
        
        private List<GameObject> loadedObjects = new List<GameObject>();
        private Environment2D loadedEnvironment;


        public async void LoadEnvironment(Environment2D env)
        {
            loadedEnvironment = env;
            ApiManager.Instance.object2D.EnvironmentId = loadedEnvironment.Id;
            List<Object2D> object2Ds = await ApiManager.Instance.ReadObject2Ds();
            foreach (var object2D in object2Ds)
            {
                int idofPrefab = int.Parse(object2D.PrefabId);
                GameObject objectToPlace = objects[idofPrefab];
                if (objectToPlace != null)
                {
                    GameObject placedObject = Instantiate(objectToPlace, this.transform);
                    placedObject.transform.position = new Vector3(object2D.PositionX, object2D.PositionY, 0);
                    placedObject.transform.rotation = Quaternion.Euler(0, 0, object2D.RotationZ);
                    placedObject.GetComponent<SpriteRenderer>().sortingOrder = object2D.SortingLayer;
                    placedObject.GetComponent<Draggable>().prefabId = object2D.PrefabId;
                    loadedObjects.Add(placedObject);
                }
                else
                {
                    Debug.LogError("Object with prefab id " + object2D.PrefabId + " not found in EnvManager.");
                }
                
            }
        }

        public void PlaceObject(string id)
        {
            GameObject objectToPlace = objects.Find(obj => obj.name == id);
            if (objectToPlace != null)
            {
                GameObject placedObject = Instantiate(objectToPlace, this.transform);
                placedObject.GetComponent<Draggable>().isDragging = true;
                placedObject.GetComponent<Draggable>().prefabId = id;
                loadedObjects.Add(placedObject);
            }
            else
            {
                Debug.LogError("Object with id " + id + " not found in EnvManager.");
            }
        }

        public void SaveObject(string id, string prefabId, float x, float y, float rotationZ, int sortingOrder, bool newObject)
        {
            ApiManager.Instance.object2D.EnvironmentId = loadedEnvironment.Id;
            ApiManager.Instance.object2D.Id = id;
            ApiManager.Instance.object2D.PrefabId = prefabId;
            ApiManager.Instance.object2D.PositionX = x;
            ApiManager.Instance.object2D.PositionY = y;
            ApiManager.Instance.object2D.RotationZ = rotationZ;
            ApiManager.Instance.object2D.SortingLayer = sortingOrder;

            if (newObject)
            {
                ApiManager.Instance.CreateObject2D();
            }
            else
            {
                ApiManager.Instance.UpdateObject2D();
            }

        }
    }
}