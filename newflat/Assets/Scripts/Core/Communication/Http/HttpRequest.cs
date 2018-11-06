using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SystemCore.Task;

public static class HttpRequest  {

	/// <summary>
	/// get请求
	/// </summary>
	/// <returns>The request.</returns>
	/// <param name="url">URL.</param>
	/// <param name="sucessCallBack">Sucess call back.</param>
	/// <param name="errorCallBack">Error call back.</param>
	public static IEnumerator GetRequest(string url,System.Action<string> sucessCallBack,System.Action<string> errorCallBack)
	{
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                errorCallBack.Invoke(www.error);
            }
            else
            {
             

                if (www.responseCode == 200)
                {

                    // Debug.Log(www.downloadHandler);
                    sucessCallBack.Invoke(www.downloadHandler.text);
                }
            }



        }


    }


	/// <summary>
	/// Posts 请求
	/// </summary>
	/// <returns>The request.</returns>
	/// <param name="url">URL.</param>
	/// <param name="postData">Post data.</param>
	/// <param name="sucessCallBack">Sucess call back.</param>
	/// <param name="errorCallBack">Error call back.</param>
	public static IEnumerator PostRequest(string url,string postData,
		System.Action<string> sucessCallBack,System.Action<string> errorCallBack)
	{

		using (UnityWebRequest www = new UnityWebRequest (url,"POST")) 
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes (postData);
			www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer ();
			www.SetRequestHeader ("Content-Type", "application/json");
			yield return www.SendWebRequest();
            if (www.error != null) {
				errorCallBack.Invoke (www.error);
			} else {
				if(www.responseCode == 200)

				{
					sucessCallBack.Invoke (www.downloadHandler.text);
				}
			}
		}
	}
}
