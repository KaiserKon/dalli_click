using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public struct QuizImage
{
    public Texture2D image { get; private set; }
    public float imageRatio { get; private set; }

    public QuizImage(Texture2D image) {
        this.image = image;
        this.imageRatio = (float)image.width / (float)image.height;
    }
}

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public static Random rnd = new Random();

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [Header("Image Settings")]
    public List<QuizImage> images = new List<QuizImage>();

    [Header("Hexagon Grid Settings")]
    [Min(0)]
    public Vector2Int gridSize;
    [Min(0)]
    public float hexagonOuterSize = 1f;
    [Min(0)]
    public float hexagonInnerSize = 0f;
    public bool isFlatTopped;
    public List<GameObject> hexagons;
}
