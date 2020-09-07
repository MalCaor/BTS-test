using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShapeGenerator
{
    // Deform the sphere

    ShapeSettings settings;

    // Constructor
    public PlanetShapeGenerator(ShapeSettings shapeSettings)
    {
        this.settings = shapeSettings;
    }

    // Calculate point
    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        // biger the radius, biger the planet
        return pointOnUnitSphere * settings.planetRadius;
    }
}
