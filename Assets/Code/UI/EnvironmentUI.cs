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
        public TMP_Text errorText;
        
        public async Task LoadEnvironments()
        {
            listModal.SetActive(true);
            foreach (EnvButton button in scrollViewContainer.GetComponentsInChildren<EnvButton>())
            {
                Destroy(button.gameObject);
            }
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


        public void loadEnvironment(Environment2D environment2D)
        {
            Debug.Log("loading environment with id: " + environment2D.Id);
            EnvManager.Instance.LoadEnvironment(environment2D);
            
            UIManager.Instance.HideAllUI();
            UIManager.Instance.showSideUI();
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
            if (environments.Exists(env => env.Name == nameField.text))
            {
                Debug.Log("Environment with name " + nameField.text + " already exists!");
                errorText.text = "Environment with name " + nameField.text + " already exists!";
                return;
                
            }

            if (nameField.text != "" || nameField.text.Length >= 25)
            {
                Debug.Log("Environment name must be between 1 and 25 characters!");
                errorText.text = "Environment name must be between 1 and 25 characters!";
                return;
            }
            
            Environment2D environment2D = new Environment2D();
            Guid guid = Guid.NewGuid();
            environment2D.Id = guid.ToString();
            environment2D.Name = nameField.text;
            environment2D.MaxHeight = 100;
            environment2D.MaxLength = 100;
            ApiManager.Instance.environment2D = environment2D;
            await ApiManager.Instance.CreateEnvironment2D();

            nameField.text = "Enter a name";
            createModal.SetActive(false);
            loadEnvironment(environment2D);
        }
    }
}