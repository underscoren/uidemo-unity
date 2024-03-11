using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using static Globals;

public class LoginScreen : MonoBehaviour
{
    [SerializeField] private UIDocument loginScreenDoc;
    private TextField _usernameTextField;
    private TextField _passwordTextField;
    private Button _loginButton;
    private Label _errorMessage;

    public UnityAction loggedIn;

    void Start()
    {
        var rootElement = loginScreenDoc.rootVisualElement;

        _usernameTextField = rootElement.Q<TextField>("username");
        _passwordTextField = rootElement.Q<TextField>("password");
        _loginButton = rootElement.Q<Button>("login");
        _errorMessage = rootElement.Q<Label>("message");
        
        _loginButton.clicked += async () =>
        {
            var loginRequest = new JSONRequest<SessionData,LoginData>();

            try
            {
                await loginRequest.Post(API_URL_BASE + "/api/login", new LoginData
                {
                    username = _usernameTextField.text,
                    password = _passwordTextField.text
                });

                var sessionData = loginRequest.ResponseData;
                
                Debug.Log("Login success");
                Debug.Log($"loginData.session={sessionData.session}");
                Debug.Log($"loginData.userID={sessionData.userID}");
                
                SessionGlobals.Instance.sessionID = sessionData.session;
                
                loggedIn.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError("Login exception");
                Debug.LogError(e);
                if (loginRequest.WebRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    _errorMessage.text = "Network error. Check your internet connection and try again.";
                }

                if (loginRequest.WebRequest.responseCode == 401)
                {
                    try
                    {
                        var errorData = JsonUtility.FromJson<ErrorMessage>(loginRequest.WebRequest.downloadHandler.text);
                        _errorMessage.text = $"Error logging in:\n{errorData.error}";
                    }
                    catch (Exception exception)
                    {
                        // ignored
                    }
                }
            }
            
        };
    }

    [Serializable]
    private class LoginData
    {
        public string username;
        public string password;
    }

    [Serializable]
    private class SessionData
    {
        public string session;
        public int userID;
    }
    
    [Serializable]
    private class ErrorMessage
    {
        public string error;
    }
}
