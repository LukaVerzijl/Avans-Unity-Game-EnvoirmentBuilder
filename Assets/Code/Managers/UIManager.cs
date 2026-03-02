using System.Threading.Tasks;
using Code.UI;
using Code.Utils;

namespace Code.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        public EnvironmentUI environment2D;
        public LoginUI loginUI;

        public async Task ShowEnvironmentUI()
        {
            environment2D.gameObject.SetActive(true);
            await environment2D.LoadEnvironments();
        }

        public void HideAllUI()
        {
            environment2D.gameObject.SetActive(false);
            loginUI.gameObject.SetActive(false);
        }
        
    }
}