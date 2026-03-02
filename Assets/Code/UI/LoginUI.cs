using System.Threading.Tasks;
using Code.Managers;
using TMPro;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Text validationText;


    private void Awake()
    {
        passwordInput.inputType = TMP_InputField.InputType.Password;
    }


    public async Task OnLoginButtonClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        Debug.Log(email);
        Debug.Log(password);
        
        ApiManager.Instance.user.Email = email;
        ApiManager.Instance.user.Password = password;
        if (await ApiManager.Instance.Login())
        {
            Debug.Log("Login succes!");
            await UIManager.Instance.ShowEnvironmentUI();
        }
        else
        {
            Debug.Log("Login failed!");
            validationText.text = "Login failed! Please check your credentials and try again.";
            
        }
        
    }

    public async Task OnRegisterButtonClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        
        ApiManager.Instance.user.Email = email;
        ApiManager.Instance.user.Password = password;
        if (await ApiManager.Instance.Register())
        {
            await  UIManager.Instance.ShowEnvironmentUI();
        }
        else
        {
            validationText.text = "Registration failed! Please check your credentials and try again.";
        }

    }
    
}
