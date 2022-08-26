using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    public TMP_InputField m_hexagonSize;
    public TMP_InputField m_gridDimensionX;
    public TMP_InputField m_gridDimensionY;

    public Button m_startBtn;

    void Awake() {
        float hexagonSize = GameController.Instance.hexagonOuterSize;
        Vector2Int gridDimensions = GameController.Instance.gridSize;

        m_hexagonSize.text = hexagonSize.ToString();
        m_gridDimensionX.text = gridDimensions.x.ToString();
        m_gridDimensionY.text = gridDimensions.y.ToString();
    }

    private void Start() {
        m_hexagonSize.onEndEdit.AddListener(OnHexagonSizeEdited);
        m_gridDimensionX.onEndEdit.AddListener(OnGridXEdited);
        m_gridDimensionY.onEndEdit.AddListener(OnGridYEdited);
    }

    void Update() {
        m_startBtn.interactable = checkStartGameConditions();
    }

    bool checkStartGameConditions() {
        var GC = GameController.Instance;
#if !UNITY_EDITOR
        return GC.images.Count > 0
            && GC.hexagonOuterSize > 0
            && GC.gridSize.x > 0
            && GC.gridSize.y > 0;
#endif
#if UNITY_EDITOR
        return true;
#endif
    }

    void OnHexagonSizeEdited(string inputValue) {
        float val = 0f;

        if (inputValue.Trim().Length != 0)
            val = float.Parse(inputValue);

        GameController.Instance.hexagonOuterSize = val;
        m_hexagonSize.text = val.ToString();
    }

    void OnGridXEdited(string inputValue) {
        int val = 0;

        if (inputValue.Trim().Length != 0)
            val = int.Parse(inputValue);

        GameController.Instance.gridSize.x = val;
        m_gridDimensionX.text = val.ToString();
    }

    void OnGridYEdited(string inputValue) {
        int val = 0;

        if (inputValue.Trim().Length != 0)
            val = int.Parse(inputValue);

        GameController.Instance.gridSize.y = val;
        m_gridDimensionY.text = val.ToString();
    }
}
