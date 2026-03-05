using System.Threading.Tasks;
using Code.Managers;
using TMPro;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Text validationText;
    private bool isLoggingIn = false;

    private void Awake()
    {
        passwordInput.inputType = TMP_InputField.InputType.Password;
    }

    public async void OnLoginButtonClicked()
    {
        if (isLoggingIn)
        {
            return;
        }
        isLoggingIn = true;
        string email = emailInput.text;
        string password = passwordInput.text;
        
        ApiManager.Instance.user.Email = email;
        ApiManager.Instance.user.Password = password;
        if (await ApiManager.Instance.Login())
        {
            Debug.Log("Login succes!");
            isLoggingIn = false;
            UIManager.Instance.ShowEnvironmentUI();
        }
        else
        {
            Debug.Log("Login failed!");
            isLoggingIn = false;
            validationText.text = "Login failed! Please check your credentials and try again.";
        }
    }

    public async void OnRegisterButtonClicked()
    {
        if (isLoggingIn) return;
        isLoggingIn = true;
        string email = emailInput.text;
        string password = passwordInput.text;
        
        ApiManager.Instance.user.Email = email;
        ApiManager.Instance.user.Password = password;
        if (await ApiManager.Instance.Register())
        { 
            isLoggingIn = false;
            UIManager.Instance.ShowEnvironmentUI();
        }
        else
        {
            isLoggingIn = false;
            validationText.text = "Registration failed! Please check your credentials and try again.";
        }
    }
}