using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AspectRatioFitter))]
public class ImagesSwitch : MonoBehaviour
{
    public Button m_nextBtn;
    RawImage m_image;
    AspectRatioFitter m_ratioFitter;
    int m_imageIndex = 0;

    private void Start() {
        m_image = GetComponent<RawImage>();
        m_ratioFitter = GetComponent<AspectRatioFitter>();

        Button btn = m_nextBtn.GetComponent<Button>();
        btn.onClick.AddListener(OnSwitchImageBtn);
    }

    public void ResetImages() {
        GameController.Instance.images.Clear();
    }

    void OnSwitchImageBtn() {
        if (m_imageIndex >= GameController.Instance.images.Count) m_imageIndex = 0;

        QuizImage qImage = GameController.Instance.images[m_imageIndex];

        m_image.texture = qImage.image;
        m_ratioFitter.aspectRatio = qImage.imageRatio;

        m_imageIndex++;
    }
}
