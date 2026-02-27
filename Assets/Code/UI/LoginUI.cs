using System;
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


    public void OnLoginButtonClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        Debug.Log(email);
        Debug.Log(password);
        
        ApiManager.Instance.user.Email = email;
        ApiManager.Instance.user.Password = password;
        ApiManager.Instance.Login();
    }

    public void OnRegisterButtonClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        
        ApiManager.Instance.user.Email = email;
        ApiManager.Instance.user.Password = password;
        ApiManager.Instance.Register();
    }
    
    
    
}
