using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;

public class FileUpload : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ImageUploaderCaptureClick();

    public TextMeshProUGUI counter;

#if !UNITY_EDITOR
    private void Start() {
        ImageUploaderCaptureClick();
    }

    private void Update() {
        if (counter != null)
            counter.text = GameController.Instance.images.Count.ToString();
    }
#endif

    IEnumerator LoadTexture(string url) {
        using UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        yield return uwr.SendWebRequest();
        if (uwr.error != null) Debug.LogError(uwr.error);
        else {
            Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
            GameController.Instance.images.Add(new QuizImage(texture));
        }
    }

    public void FileSelected(string url) {
        StartCoroutine(LoadTexture(url));
    }

    public void ResetImages() {
        GameController.Instance.images.Clear();
    }
}