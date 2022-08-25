using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AspectRatioFitter))]
public class ImagesSwitch : MonoBehaviour
{
    public Button m_nextBtn;
    public Button m_resetBtn;
    RawImage m_image;
    AspectRatioFitter m_ratioFitter;
    int m_imageIndex = 0;

    private void Start() {
        m_image = GetComponent<RawImage>();
        m_ratioFitter = GetComponent<AspectRatioFitter>();

        Button nextBtn = m_nextBtn.GetComponent<Button>();
        nextBtn.onClick.AddListener(OnSwitchImageBtn);

        Button resetBtn = m_resetBtn.GetComponent<Button>();
        resetBtn.onClick.AddListener(OnResetImages);
    }

    void OnResetImages() {
        GameController.Instance.images.Clear();
    }

    void OnSwitchImageBtn() {
        if (GameController.Instance.images.Count == 0) return;

        if (m_imageIndex >= GameController.Instance.images.Count) m_imageIndex = 0;

        QuizImage qImage = GameController.Instance.images[m_imageIndex];

        m_image.texture = qImage.image;
        m_ratioFitter.aspectRatio = qImage.imageRatio;

        m_imageIndex++;
    }
}
