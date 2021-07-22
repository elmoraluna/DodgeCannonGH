using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Circle
{
    public static void DrawCircle(this GameObject container, float radius, float lineWidth)
    {
        Color c1 = Color.black;
        //Color c2 = new Color(0, 0, 0, 1);
        var segments = 360;
        var line = container.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        /*line.startColor = c1;
        line.endColor = c1;*/
        line.material.color = Color.black;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }
}
