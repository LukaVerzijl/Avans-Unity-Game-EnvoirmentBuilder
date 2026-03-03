using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class EnvironmentUI : MonoBehaviour
    {
        public GameObject scrollViewContainer;
        public GameObject environmentButtonPrefab;
        public GameObject createButton;
        public GameObject listModal;
        public GameObject createModal;
        public TMP_InputField nameField;
        private List<Environment2D> environments;
        
        public async Task LoadEnvironments()
        {
            environments = await ApiManager.Instance.ReadEnvironment2Ds();
            if (environments.Count > 0)
            {
                foreach (Environment2D environment in environments)
                {
                    GameObject environmentButtonObject = Instantiate(environmentButtonPrefab, scrollViewContainer.transform);
                    EnvButton envButton = environmentButtonObject.GetComponent<EnvButton>();
                    envButton.setEnvironmentText(environment.Name, environment.updatedAt.ToShortDateString());
                    envButton.environment2D = environment;
                }
            }

            if (environments.Count >= 5)
            {
                createButton.SetActive(false);
            }
        }


        public async Task loadEnvironment(Environment2D environment2D)
        {
            Debug.Log("loading environment with id: " + environment2D.Id);
        }
        

        public void OpenCreateEnvironmentUI()
        {
            listModal.SetActive(false);
            createModal.SetActive(true);
        }

        public async void createEnvironment()
        {
            if (environments.Count >= 5)
                return;
            
            Environment2D environment2D = new Environment2D();
            Guid guid = Guid.NewGuid();
            environment2D.Id = guid.ToString();
            environment2D.Name = nameField.text;
            environment2D.MaxHeight = 100;
            environment2D.MaxLength = 100;
            ApiManager.Instance.environment2D = environment2D;
            await ApiManager.Instance.CreateEnvironment2D();
            
            await loadEnvironment(environment2D);
        }
    }
}