using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0618

namespace CoreLib.Extensions
{
    public static class Extensions_LineRenderer
    {
        //Just lets us draw circles easily
    
        public static void RenderCircle(this LineRenderer l, float radius , int sides = 512, float ZOverride = 0)
        {
            int numSegments = sides;
            l.material = new Material(Shader.Find("Sprites/Default"));
            l.SetWidth(0.025f, 0.025f);
            l.SetVertexCount(numSegments + 1);
            l.useWorldSpace = false;
            l.sortingOrder = -99;

            float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
            float theta = 0f;

            for (int i = 0 ; i < numSegments + 1 ; i++) {
                float x = radius * Mathf.Cos(theta);
                float z = radius * Mathf.Sin(theta);
                Vector3 pos = new Vector3(x, z, ZOverride); //check which coordinates are used if things arent working
                l.SetPosition(i, pos);
                theta += deltaTheta;
            }
        }
	
        public static void RenderCircle(this LineRenderer l, float radius, Color color, float thickness = 0.025f, float zOverride = 0f)
        {
            int numSegments = 512;
            Material m = new Material(Shader.Find("Sprites/Default"));
            m.SetColor ("_EmissionColor", color);

            m.EnableKeyword ("_EMISSION");
            l.material = m;
            l.SetColors(color, color);
            l.SetWidth(thickness, thickness);
            l.SetVertexCount(numSegments + 1);
            l.useWorldSpace = false;
            l.sortingOrder = -99;

            float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
            float theta = 0f;

            for (int i = 0 ; i < numSegments + 1 ; i++) {
                float x = radius * Mathf.Cos(theta);
                float z = radius * Mathf.Sin(theta);
                Vector3 pos = new Vector3(x, z, zOverride);
                l.SetPosition(i, pos);
                theta += deltaTheta;
            }
        }

        public static List<Vector3> GetCirclePositions(float radius, float zOverride = 0f)
        {
            int numSegments = 512;
            List<Vector3> results = new List<Vector3>();
            float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
            float theta = 0f;

            for (int i = 0 ; i < numSegments + 1 ; i++) {
                float x = radius * Mathf.Cos(theta);
                float z = radius * Mathf.Sin(theta);
                Vector3 pos = new Vector3(x, z, zOverride);
                results.Add(pos);
                theta += deltaTheta;
            }
            return results;
        }
    
        public static void RenderCircle3D(this LineRenderer l, float radius, Color color, float thickness = 0.025f, float zOverride = 0f)
        {
            int numSegments = 512;
            l.material = new Material(Shader.Find("Sprites/Default"));
            l.SetColors(color, color);
            l.SetWidth(thickness, thickness);
            l.SetVertexCount(numSegments + 1);
            l.useWorldSpace = false;
            l.sortingOrder = -99;

            float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
            float theta = 0f;

            for (int i = 0 ; i < numSegments + 1 ; i++) {
                float x = radius * Mathf.Cos(theta);
                float z = radius * Mathf.Sin(theta);
                Vector3 pos = new Vector3(x, zOverride, z);
                l.SetPosition(i, pos);
                theta += deltaTheta;
            }
        }
    
        public static void RenderDottedCircle(this LineRenderer l, float radius, Color color, int sides = 512)
            //TODO
        {
            int numSegments = sides;
            l.material = UnityEngine.Resources.Load<Material>("Sprites/Default");
            //   l.materials[0].mainTextureScale = new Vector3(128, 1 , 1);
            l.SetColors(color, color);
            l.SetWidth(0.025f, 0.025f);
            l.SetVertexCount(numSegments + 1);
            l.useWorldSpace = false;

            float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
            float theta = 0f;

            for (int i = 0 ; i < numSegments + 1 ; i++) {
                float x = radius * Mathf.Cos(theta);
                float z = radius * Mathf.Sin(theta);
                Vector3 pos = new Vector3(x, 0, z);
                l.SetPosition(i, pos);
                theta += deltaTheta;
            }
        }
    
        public static void Initialize(this LineRenderer l)
        {
            l.material = new Material(Shader.Find("Sprites/Default"));
            l.SetWidth(0.025f, 0.025f);
            l.useWorldSpace = false;
            l.sortingOrder = -99;
        }
    }
}
