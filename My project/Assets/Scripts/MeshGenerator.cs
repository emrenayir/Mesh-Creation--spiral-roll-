using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator: MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    public float unitLength; //Min lenght of a sprial
    public float thickness; //Thickness of a spiral

    public Vector3 startingEulerAngles; //This object will be rotated at the start
    public Vector3 rotationEulerAngles; //The mesh will be rotated this much at each update
    public Vector3 spiralVelocity;//When mouse click up spiral will have this velocity
    public float spiralLifeSpan; //Spiral will be destroy after this many seconds

    private float tZero; //time at the start 
    private float tEnd; //time when mouse click up
    private bool spatulaIsGrounded; // True if spatule grounded
    private Quaternion rotation; //Mesh will be rotated each update this much

    private void Start()
    {
        transform.eulerAngles = startingEulerAngles;

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateMesh();
        UpdateMesh();

        spatulaIsGrounded = true;
        tZero = Time.time;
        
        rotation = new Quaternion();
        rotation.eulerAngles = rotationEulerAngles;
    }

    private void Update()
    {
        if (spatulaIsGrounded)
        {
            //rotate existing mesh
            Rotate(rotation,Vector3.zero);
            
            Shift(new Vector3(0,unitLength + (Time.time -tZero)/10, 0));

            ExtrudeSpiral();
            UpdateMesh();
        }
        else
        {
            if (Time.time -tEnd > spiralLifeSpan)
            {
                Destroy(gameObject);
            }
        }
    }


    //Create initial vertices and triangles(rectengular prism, with size 1 unitlegend thickness)
    private void CreateMesh()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,unitLength,0),
            new Vector3(1,unitLength,0),
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(0,unitLength,thickness),
            new Vector3(1,unitLength,thickness),
            new Vector3(0,0,thickness),
            new Vector3(1,0,thickness),
        };

        triangles = new int[] {3, 2, 0, 3, 0, 1, 7, 4, 6, 7, 5, 4};
    }
    
    
    private void UpdateMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void Rotate(Quaternion rotation, Vector3 center)
    {
        int count = vertices.Length;
        for (int i = 0; i < count; i++)
        {
            vertices[i] = rotation * (vertices[i] - center) + center;
        }
    }

    private void Shift(Vector3 offset)
    {
        int count = vertices.Length;
        for (int i = 0; i < count; i++)
        {
            vertices[i] = vertices[i] + offset;
        }
    }
    
    //adds another "piece" to the spiral
    private void ExtrudeSpiral()
    {
        int vertexCount = vertices.Length;
        int triangleCount = triangles.Length;
        
        
        Vector3[] newVertices = new Vector3[vertexCount + 4];
        int[] newTriangles = new int[triangleCount + 12];

        for (int i = 0; i < vertexCount; i++)
        {
            newVertices[i] = vertices[i];
        }

        for (int i = 0; i < triangleCount; i++)
        {
            newTriangles[i] = triangles[i];
        }

        newVertices[vertexCount] = new Vector3(0, 0, 0);
        newVertices[vertexCount+1] = new Vector3(1, 0, 0);
        newVertices[vertexCount+2] = new Vector3(0, 0, thickness);
        newVertices[vertexCount+3] = new Vector3(1, 0, thickness);

        newTriangles[triangleCount] = vertexCount;
        newTriangles[triangleCount +1] = vertexCount -3;
        newTriangles[triangleCount +2] = vertexCount +1;
        newTriangles[triangleCount +3] = vertexCount;
        newTriangles[triangleCount +4] = vertexCount -4;
        newTriangles[triangleCount +5] = vertexCount -3;

        newTriangles[triangleCount + 6] = vertexCount +2;
        newTriangles[triangleCount + 7] = vertexCount +3;
        newTriangles[triangleCount + 8] = vertexCount -1;
        newTriangles[triangleCount + 9] = vertexCount +2;
        newTriangles[triangleCount + 10] = vertexCount -1;
        newTriangles[triangleCount + 11] = vertexCount -2;

        vertices = newVertices;
        triangles = newTriangles;
    }

    public void StopScraping()
    {
        spatulaIsGrounded = false;

        tEnd = Time.time;

        CapsuleCollider col = GetComponent<CapsuleCollider>();

        if (vertices.Length < 50)
        {
            col.center = (vertices[0] + vertices[1]) / 2;
        }
        else
        {
            col.center = (vertices[0] + vertices[49]) / 2;
        }

        col.radius = Vector3.Distance(col.center, vertices[vertices.Length - 5]) - 0.05f;
        col.enabled = true;

        GetComponent<Rigidbody>().velocity = spiralVelocity;
    }
    

}
