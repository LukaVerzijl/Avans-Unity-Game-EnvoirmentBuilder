using System.Collections.Generic;
using Code.Utils;
using UnityEngine;

namespace Code.Managers
{
    public class EnvManager : Singleton<EnvManager>
    {
        public List<GameObject> objects = new List<GameObject>();
        
        public List<GameObject> loadedObjects = new List<GameObject>();
        private Environment2D loadedEnvironment;


        public async void LoadEnvironment(Environment2D env)
        {
            loadedEnvironment = env;
            ApiManager.Instance.object2D.GameEnvironmentId = loadedEnvironment.Id;
            List<Object2D> object2Ds = await ApiManager.Instance.ReadObject2Ds();
            Debug.Log(object2Ds.Count);
            if (object2Ds.Count != 0 && object2Ds != null)
            {
                foreach (var object2D in object2Ds)
                {
                    int idofPrefab = int.Parse(object2D.PrefabId);
                    GameObject objectToPlace = objects[idofPrefab];
                    if (objectToPlace != null)
                    {
                        GameObject placedObject = Instantiate(objectToPlace, this.transform);
                        placedObject.transform.position = new Vector3(object2D.PositionX, object2D.PositionY, 0);
                        placedObject.transform.rotation = Quaternion.Euler(0, 0, object2D.RotationZ);
                        // placedObject.GetComponent<SpriteRenderer>().sortingOrder = object2D.SortingLayer;
                        placedObject.GetComponent<Draggable>().prefabId = object2D.PrefabId;
                        placedObject.GetComponent<Draggable>().id = object2D.Id;
                        placedObject.GetComponent<Draggable>().isNew = false;
                        loadedObjects.Add(placedObject);
                    }
                    else
                    {
                        Debug.LogError("Object with prefab id " + object2D.PrefabId + " not found in EnvManager.");
                    }

                }
            }
        }

        public void PlaceObject(string id)
        {
            GameObject objectToPlace = objects[int.Parse(id)];
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

        public async void SaveObject(string id, string prefabId, float x, float y, float rotationZ, int sortingOrder, bool newObject)
        {
            ApiManager.Instance.object2D.GameEnvironmentId = loadedEnvironment.Id;
            ApiManager.Instance.object2D.Id = id;
            ApiManager.Instance.object2D.PrefabId = prefabId;
            ApiManager.Instance.object2D.PositionX = x;
            ApiManager.Instance.object2D.PositionY = y;
            ApiManager.Instance.object2D.RotationZ = rotationZ;
            ApiManager.Instance.object2D.SortingLayer = sortingOrder;

            if (newObject)
            {
                await ApiManager.Instance.CreateObject2D();
            }
            else
            {
                await ApiManager.Instance.UpdateObject2D();
            }

        }
        
        public async void DeleteObject(string id)
        {
            ApiManager.Instance.object2D.Id = id;
            await ApiManager.Instance.DeleteObject2D();
            int index = loadedObjects.FindIndex(o => o.GetComponent<Draggable>().id == id);
            
            if (index != -1)
            {
                GameObject objectToDelete = loadedObjects[index];
                loadedObjects.RemoveAt(index);
                Destroy(objectToDelete);
            }
            else
            {
                Debug.LogError("Object with id " + id + " not found in loadedObjects.");
            }
        }

        public void DeleteEnvoirment(string id)
        {
            ApiManager.Instance.environment2D.Id = id;
            ApiManager.Instance.DeleteEnvironment2D();
        }

        public void UnloadEnvironment()
        {
            foreach (var obj in loadedObjects)
            {
                Destroy(obj);
            }
            loadedObjects.Clear();

            loadedObjects.Clear();
            loadedEnvironment = null;

            UIManager.Instance.HideAllUI();
            UIManager.Instance.ShowEnvironmentUI();
        }
    }
}