using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebCallScript : MonoBehaviour
{
    UnityWebRequest _request;
    Image _myImage;

    internal void GetImmage(string URL, Image targetImage)
    {
        _myImage = targetImage;
        StartCoroutine(GetImmageFromURL(URL));
    }

    IEnumerator GetImmageFromURL(string URL)
    {
        _request = UnityWebRequestTexture.GetTexture(URL);
        yield return _request.SendWebRequest();

        if (_request.error == null)
        {
            Texture2D ImageBuffer = ((DownloadHandlerTexture)_request.downloadHandler).texture;
            _myImage.sprite = Sprite.Create(ImageBuffer, new Rect(0.0f, 0.0f, ImageBuffer.width, ImageBuffer.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.Log(_request.error);
        }
    }
}