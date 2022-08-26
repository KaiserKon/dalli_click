using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Face
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    public List<Vector2> uvs { get; private set; }

    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs) {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;
    }
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(OffSceneDestroyer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;

    private List<Face> m_faces;

    private float m_innerSize;
    private float m_outerSize;
    private bool m_isFlatTopped;

    private void Awake() {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_mesh = new Mesh();
        m_mesh.name = "Hexagon";

        m_meshFilter.mesh = m_mesh;
    }

    public void SetParamsAndDraw(float outerSize, float innerSize, Material material, bool isFlatTopped = false) {
        m_outerSize = outerSize;
        m_innerSize = innerSize;
        m_isFlatTopped = isFlatTopped;

        m_meshRenderer.material = material;

        DrawMesh();
    }

    public void DrawMesh() {
        DrawFaces();
        CombineFaces();
    }

    private void DrawFaces() {
        m_faces = new List<Face>();

        for (int point = 0; point < 6; point++)
            m_faces.Add(CreateFace(m_innerSize, m_outerSize, point));
    }

    private void CombineFaces() {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < m_faces.Count; i++) {
            vertices.AddRange(m_faces[i].vertices);
            uvs.AddRange(m_faces[i].uvs);

            int offset = (4 * i);
            foreach (int triangle in m_faces[i].triangles)
                tris.Add(triangle + offset);
        }

        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = tris.ToArray();
        m_mesh.uv = uvs.ToArray();
        m_mesh.RecalculateNormals();

    }

    private Face CreateFace(float innerRad, float outerRad, int point, bool reverse = false) {
        Vector3 pointA = GetPoint(innerRad, point);
        Vector3 pointB = GetPoint(innerRad, (point < 5) ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRad, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRad, point);

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

        if (reverse)
            vertices.Reverse();

        return new Face(vertices, triangles, uvs);
    }

    protected Vector3 GetPoint(float size, int index) {
        float angle_deg = m_isFlatTopped ? 60 * index : 60 * index - 30;
        float angle_rad = Mathf.PI / 180f * angle_deg;
        return new Vector3((size * Mathf.Cos(angle_rad)), 0, (size * Mathf.Sin(angle_rad)));
    }
}
