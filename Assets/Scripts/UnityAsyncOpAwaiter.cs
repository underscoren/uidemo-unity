using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// from: https://gist.github.com/mattyellen/d63f1f557d08f7254345bff77bfdc8b3
// Makes UnityWebRequests await-able via TaskAwaiter

public static class ExtensionMethods
{
    public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
    {
        var tcs = new TaskCompletionSource<object>();
        asyncOp.completed += _ => { tcs.SetResult(null); };
        return ((Task)tcs.Task).GetAwaiter();
    }
}

/* Example:
var getRequest = UnityWebRequest.Get("http://www.google.com");
await getRequest.SendWebRequest();
var result = getRequest.downloadHandler.text;
*/