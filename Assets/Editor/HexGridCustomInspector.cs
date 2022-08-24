using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexGrid))]
public class HexGridCustomInspector : Editor
{
    public override void OnInspectorGUI() {
        HexGrid hexGrid = (HexGrid)target;

        if (GUILayout.Button("Generate new Grid"))
            hexGrid.LayoutGrid();

        if (GUILayout.Button("Delete random Hexagon"))
            hexGrid.deletRandomHexagon();

        if (GUILayout.Button("Check Visibility"))
            hexGrid.checkTileVisibility();


        base.OnInspectorGUI();
    }
}
