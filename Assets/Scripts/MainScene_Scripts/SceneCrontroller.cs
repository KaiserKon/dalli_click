using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class SceneCrontroller : MonoBehaviour
{
    public TextMeshProUGUI m_score;
    public ImagesSwitch m_imageSwitcher;

    [DllImport("__Internal")]
    private static extern void RemoveUploadButton();

    private void Start() {
#if !UNITY_EDITOR
        RemoveUploadButton();
        m_imageSwitcher.nextImage();
#endif
    }

    void Update()
    {
        m_score.text = GameController.Instance.hexagons.Count.ToString();
    }
}
