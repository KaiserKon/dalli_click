using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AspectRatioFitter))]
public class ImagesSwitch : MonoBehaviour
{
    RawImage m_image;
    AspectRatioFitter m_ratioFitter;
    int m_imageIndex = 0;

    private void Awake() {
        m_image = GetComponent<RawImage>();
        m_ratioFitter = GetComponent<AspectRatioFitter>();
    }

    public void nextImage() {
        if (GameController.Instance.images.Count == 0) return;

        if (m_imageIndex >= GameController.Instance.images.Count) m_imageIndex = 0;

        QuizImage qImage = GameController.Instance.images[m_imageIndex];

        m_image.texture = qImage.image;
        m_ratioFitter.aspectRatio = qImage.imageRatio;

        m_imageIndex++;
    }
}
