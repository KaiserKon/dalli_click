using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [Header("Tile Settings")]
    public Material material;

    private void Start() {
        LayoutGrid();
    }

    public void LayoutGrid() {
        if (GameController.Instance.hexagons.Count > 0) {
            foreach (GameObject gameObject in GameController.Instance.hexagons)
                Destroy(gameObject);

            GameController.Instance.hexagons.Clear();
        }

        for (int y = 0; y < GameController.Instance.gridSize.y; y++) {
            for (int x = 0; x < GameController.Instance.gridSize.x; x++) {
                GameObject tile = new GameObject($"Hexagon ({x},{y})", typeof(HexRenderer));
                tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x, y));

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.SetParamsAndDraw(GameController.Instance.hexagonOuterSize, GameController.Instance.hexagonInnerSize, material, GameController.Instance.isFlatTopped);

                tile.transform.SetParent(transform, false);
                GameController.Instance.hexagons.Add(tile);
            }
        }

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.transform.position.z * (-1)));
    }

    public void deletRandomHexagon() {
        if (GameController.Instance.hexagons.Count <= 0)
            return;

        int r = GameController.rnd.Next(GameController.Instance.hexagons.Count);

        Destroy(GameController.Instance.hexagons[r]);
        GameController.Instance.hexagons.RemoveAt(r);
    }

    public void deleteAllHexagons() {
        if (GameController.Instance.hexagons.Count <= 0)
            return;

        foreach (GameObject hexagon in GameController.Instance.hexagons) {
            Destroy(hexagon);
        }

        GameController.Instance.hexagons.Clear();
    }

    public void checkTileVisibility() {
        List<GameObject> nonVisible = GameController.Instance.hexagons.FindAll(hexagon => !hexagon.GetComponent<Renderer>().isVisible);
        nonVisible.ForEach(hexagon => {
            GameController.Instance.hexagons.Remove(hexagon);
            Destroy(hexagon);
        });
    }

    private Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate) {
        int column = coordinate.x;
        int row = coordinate.y;
        float width;
        float height;
        float xPosition;
        float yPosition;
        bool shouldOffset;
        float horizontalDist;
        float verticalDist;
        float size = GameController.Instance.hexagonOuterSize;
        float offset;

        if (GameController.Instance.isFlatTopped) {
            shouldOffset = (column % 2) == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3f) * size;

            horizontalDist = width * (3f / 4f);
            verticalDist = height;

            offset = shouldOffset ? height / 2 : 0;

            xPosition = column * horizontalDist;
            yPosition = ((row * verticalDist) - offset);
        }
        else {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDist = width;
            verticalDist = height * (3f / 4f);

            offset = shouldOffset ? width / 2 : 0;

            xPosition = ((column * horizontalDist) + offset );
            yPosition = row * verticalDist;
        }

        return new Vector3(xPosition, 0, -yPosition);
    }
}
