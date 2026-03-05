using Code.Managers;
using UnityEngine;

namespace Code.UI
{
    public class SideUI : MonoBehaviour
    {
            public void onCloseButtonClick()
            {
                UIManager.Instance.hideSideUI();
            }
            
            public void onSideUIPlaceOject(string id)
            {
                EnvManager.Instance.PlaceObject(id);
            }

            public void BackToMainMenu()
            {
                EnvManager.Instance.UnloadEnvironment();
            }
            
    }
}