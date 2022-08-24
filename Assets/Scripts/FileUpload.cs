using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Runtime.InteropServices;

public class FileUpload : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ImageUploaderCaptureClick();

    IEnumerator LoadTexture(string url) {
        using UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        yield return uwr.SendWebRequest();
        Debug.Log(uwr);
        if (uwr.error != null) Debug.LogError(uwr.error);
        else {
            Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
            GameController.Instance.images.Add(new QuizImage(texture));
        }
    }

    public void FileSelected(string url) {
        StartCoroutine(LoadTexture(url));
    }

    public void OnButtonPointerDown() {
        ImageUploaderCaptureClick();
    }
}