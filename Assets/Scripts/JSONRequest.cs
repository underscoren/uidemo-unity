
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// A wrapper to simplify making JSON HTTP Requests
/// </summary>
public class JSONRequest<TResponseData>
{
    /// <summary>
    /// Response data parsed from JSON
    /// </summary>
    public TResponseData ResponseData;
    /// <summary>
    /// The underlying <c>UnityWebRequest</c>
    /// </summary>
    public UnityWebRequest WebRequest;
    
    public async Task Get(string url)
    {
        WebRequest = UnityWebRequest.Get(url);

        await WebRequest.SendWebRequest();
        
        if (WebRequest.result != UnityWebRequest.Result.Success)
        {
            throw new Exception("WebRequest result is not Success");
        }

        string resultText = WebRequest.downloadHandler.text;
        // Debug.Log(resultText);
        ResponseData = JsonUtility.FromJson<TResponseData>(resultText);
    }
    
    public async Task Post(string url)
    {
        WebRequest = UnityWebRequest.Post(url, "", "application/json");
        
        await WebRequest.SendWebRequest();
        
        if (WebRequest.result != UnityWebRequest.Result.Success)
        {
            throw new Exception("WebRequest result is not Success");
        }
        
        Debug.Log(WebRequest.result);
        Debug.Log(WebRequest.responseCode);

        string resultText = WebRequest.downloadHandler.text;
        // Debug.Log(resultText);
        ResponseData = JsonUtility.FromJson<TResponseData>(resultText);
    }
    
    public async Task Post(string url, List<KeyValuePair<string,string>> cookies)
    {
        WebRequest = UnityWebRequest.Post(url,"","");
        
        List<string> cookieStrings = new List<string>(cookies.Count);
        foreach (var (key, value) in cookies)
        {
            cookieStrings.Add($"{key}={value}");
        }

        string cookieString = string.Join(";", cookieStrings);
        
        WebRequest.SetRequestHeader("Cookie",cookieString);
        
        await WebRequest.SendWebRequest();
        
        if (WebRequest.result != UnityWebRequest.Result.Success)
        {
            throw new Exception("WebRequest result is not Success");
        }
        
        // Debug.Log(WebRequest.result);
        // Debug.Log(WebRequest.responseCode);

        string resultText = WebRequest.downloadHandler.text;
        // Debug.Log(resultText);
        ResponseData = JsonUtility.FromJson<TResponseData>(resultText);
    }
}

/// <summary>
/// A wrapper to simplify making JSON HTTP POST Requests
/// </summary>
public class JSONRequest<TResponseData, TRequestData>
{
    /// <summary>
    /// Response data parsed from JSON. Will be null until the <c>WebRequest</c> completes successfully
    /// </summary>
    public TResponseData ResponseData;
    /// <summary>
    /// The underlying <c>UnityWebRequest</c>
    /// </summary>
    public UnityWebRequest WebRequest;
    
    public async Task Post(string url, TRequestData requestData)
    {
        // convert requestData object to JSON string
        string jsonText = JsonUtility.ToJson(requestData);
        
        WebRequest = UnityWebRequest.Post(url,jsonText,"application/json");
        
        await WebRequest.SendWebRequest();
        
        if (WebRequest.result != UnityWebRequest.Result.Success)
        {
            throw new Exception("WebRequest result is not Success");
        }
        
        // Debug.Log(WebRequest.result);
        // Debug.Log(WebRequest.responseCode);

        string resultText = WebRequest.downloadHandler.text;
        // Debug.Log(resultText);
        ResponseData = JsonUtility.FromJson<TResponseData>(resultText);
    }
    

    public async Task Post(string url, TRequestData requestData, List<KeyValuePair<string,string>> cookies)
    {
        // convert requestData object to JSON string
        string jsonText = JsonUtility.ToJson(requestData);
        
        WebRequest = UnityWebRequest.Post(url,jsonText,"application/json");

        List<string> cookieStrings = new List<string>(cookies.Count);
        foreach (var (key, value) in cookies)
        {
            cookieStrings.Add($"{key}={value}");
        }
        
        WebRequest.SetRequestHeader("Cookie",string.Join(";", cookieStrings));
        
        await WebRequest.SendWebRequest();
        
        if (WebRequest.result != UnityWebRequest.Result.Success)
        {
            throw new Exception("WebRequest result is not Success");
        }
        
        // Debug.Log(WebRequest.result);
        // Debug.Log(WebRequest.responseCode);

        string resultText = WebRequest.downloadHandler.text;
        // Debug.Log(resultText);
        ResponseData = JsonUtility.FromJson<TResponseData>(resultText);
    }
}