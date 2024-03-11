using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Globals;

public class CountScreen : MonoBehaviour
{
    [SerializeField] private UIDocument countScreenDoc;
    private Label _countLabel;
    private Button _countButton;
    

    void Start()
    {
        var rootElement = countScreenDoc.rootVisualElement;

        _countLabel = rootElement.Q<Label>("count");
        _countButton = rootElement.Q<Button>("button");

        _countButton.clicked += async () =>
        {
            var countRequest = new JSONRequest<CountData>();

            var cookies = new List<KeyValuePair<string, string>> { new("session",SessionGlobals.Instance.sessionID) };

            await countRequest.Post(API_URL_BASE + "/api/count", cookies);

            var countData = countRequest.ResponseData;
            _countLabel.text = $"Count: {countData.count}";
            
            Debug.Log("Count success");
        };
    }

    [Serializable]
    private class CountData
    {
        public int count;
    }
}
