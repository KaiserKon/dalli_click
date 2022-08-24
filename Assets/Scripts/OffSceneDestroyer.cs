using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSceneDestroyer : MonoBehaviour
{
    private Renderer m_Renderer;
    private bool canceled;

    private void Start() {
        m_Renderer = GetComponent<Renderer>();
        Invoke("DestroyGameObject", 0.5f);
    }

    private void Update() {
        if (!canceled)
            checkVisibility();
    }

    public void checkVisibility() {
        if (m_Renderer.isVisible) {
            CancelInvoke("DestroyGameObject");
            canceled = true;
        }
    }

    private void DestroyGameObject() {
        GameController.Instance.hexagons.Remove(gameObject);
        Destroy(gameObject);
    }
}
