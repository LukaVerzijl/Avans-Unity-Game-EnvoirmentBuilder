using Code.Managers;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class EnvButton : MonoBehaviour
    {
        
        public TMP_Text environmentName;
        public TMP_Text environmentChangedAt;
        public Environment2D environment2D;
        public void OnButtonClick()
        {
            UIManager.Instance.environment2D.loadEnvironment(environment2D);
        }
        
        public void setEnvironmentText(string title, string changedAt)
        {
            environmentName.text = title;
            environmentChangedAt.text = changedAt;
        }
        
        public void OnDeleteButtonClick()
        {
            EnvManager.Instance.DeleteEnvoirment(environment2D.Id);
            Destroy(this.gameObject);
        }
    }
}