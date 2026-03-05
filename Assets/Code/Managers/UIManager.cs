using System;
using System.Threading.Tasks;
using Code.UI;
using Code.Utils;

namespace Code.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        public EnvironmentUI environment2D;
        public LoginUI loginUI;
        public SideUI sideUI;
        public EditUI editUI;

        private void Start()
        {
            showLoginUI();
        }

        public async void ShowEnvironmentUI()
        {
            environment2D.gameObject.SetActive(true);
            await environment2D.LoadEnvironments();
        }

        public void showLoginUI()
        {
            loginUI.gameObject.SetActive(true);
        }

        public void HideAllUI()
        {
            environment2D.gameObject.SetActive(false);
            loginUI.gameObject.SetActive(false);
            sideUI.gameObject.SetActive(false);
            editUI.gameObject.SetActive(false);
        }

        public void showSideUI()
        {
            sideUI.gameObject.SetActive(true);
        }

        public void hideSideUI()
        {
            sideUI.gameObject.SetActive(false);
        }
        
        
        
    }
}