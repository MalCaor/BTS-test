using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // Planet Class (name is explicit i think)

    // Vars
    [Range(2, 256)]
    public int resolution = 10;
    // meshFilters contain the 6 faces of the planet (it's a cube triked into a sphere :p )
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    PlanetFaceMesh[] planetFaces;

    // OnValidate is the order of call
    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    // Initialize... initialize the planet
    void Initialize()
    {
        // declare 6 mesh faces
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        planetFaces = new PlanetFaceMesh[6];

        // Create LocalUp
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < meshFilters.Length; i++) // i use .Lenght instead of 6 because... we never know
        {
            if (meshFilters[i] == null)
            {
                // Create the GameObj of the face
                GameObject meshObj = new GameObject(string.Concat("face", i.ToString()));
                // link them to parent
                meshObj.transform.parent = transform;

                // create a mesh renderer
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            // Create the Face
            planetFaces[i] = new PlanetFaceMesh(meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    // Generate Mesh
    void GenerateMesh()
    {
        foreach (PlanetFaceMesh face in planetFaces)
        {
            face.ConstructMesh();
        }
    }
}
