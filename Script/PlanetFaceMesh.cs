using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thank to Sebastian Lague for his very helpfull tuto
// https://www.youtube.com/watch?v=QN39W020LqU&list=PLFt_AvWsXl0cONs3T0By4puYy6GM22ko8&index=1

public class PlanetFaceMesh
{
    // This Class creat the mesh of the planet face

    // Vars
    Mesh mesh;
    int resolution;
    // way it's facing
    Vector3 localUp;
    // axisB and A are perpendicular direction of the localUp
    Vector3 axisA;
    Vector3 axisB;

    // Constructor
    public PlanetFaceMesh(Mesh mesh, int resolution, Vector3 localUp)
    {
        // Set params
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        // deduce axisA and B
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA); // Cross is a fonc that search perp vector so the perp Vector of Up and A is B
    }

    // Meth Construct Mesh
    public void ConstructMesh()
    {
        // Create an array of vertices (square of res)
        Vector3[] vertices = new Vector3[resolution * resolution];
        // create an array of triangles ((square of res - 1 on each side) * (2 triangles on each face + 3 vertices that compose a triangle))
        int[] triangles = new int[(resolution-1) * (resolution-1) * 6];
        // index
        int triIndex = 0;

        // Loop throught the resolution square
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                // the place we are on the loop
                int i = x+y*resolution;
                // percentage of completion
                Vector2 percent = new Vector2(x, y) / (resolution-1);
                // set the coor of the point
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                // now trick it into a sphere
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                // set the vercite i the value of pointOnUnitCube
                vertices[i] = pointOnUnitSphere;

                // create triangles
                if (x != resolution-1 && y != resolution-1) // dont create triangle on the last x and y ligne
                {
                    // Create a square with 2 triangles

                    // first tri
                    triangles[triIndex] = i;
                    triangles[triIndex+1] = i + resolution + 1;
                    triangles[triIndex+2] = i + resolution;
                    // sec tri
                    triangles[triIndex+3] = i;
                    triangles[triIndex+4] = i + 1;
                    triangles[triIndex+5] = i + resolution + 1;

                    // increment triIndex by 6
                    triIndex += 6;
                }
            }
        }
        // Unassign the stuff in mesh
        mesh.Clear();
        // assign to mesh the vertices and triangles
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        // Recalc all
        mesh.RecalculateNormals();
    }
}
