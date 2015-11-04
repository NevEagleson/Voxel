//MeshData.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Voxel
{
    public class MeshData
    {
        public List<Vector3> Vertices { get; protected set; }
        public List<int> Triangles { get; protected set; }
        public List<Vector2> UV { get; protected set; }

        public List<Vector3> ColVertices  { get; protected set; }
        public List<int> ColTriangles { get; protected set; }

        public MeshData()
        {
            Vertices = new List<Vector3>();
            Triangles = new List<int>();
            UV = new List<Vector2>();

            ColVertices = new List<Vector3>();
            ColTriangles = new List<int>();
        }

        public void AddVertex(Vector3 vert, bool add2Render = true, bool add2Col = true)
        {
            if (add2Render)
                Vertices.Add(vert);
            if (add2Col)
                ColVertices.Add(vert);
        }

        public void AddQuadTriangles(bool add2Render = true, bool add2Col = true)
        {
            if (add2Render)
            {
                Triangles.Add(Vertices.Count - 4);
                Triangles.Add(Vertices.Count - 3);
                Triangles.Add(Vertices.Count - 2);

                Triangles.Add(Vertices.Count - 4);
                Triangles.Add(Vertices.Count - 2);
                Triangles.Add(Vertices.Count - 1);
            }
            if (add2Col)
            {
                ColTriangles.Add(ColVertices.Count - 4);
                ColTriangles.Add(ColVertices.Count - 3);
                ColTriangles.Add(ColVertices.Count - 2);

                ColTriangles.Add(ColVertices.Count - 4);
                ColTriangles.Add(ColVertices.Count - 2);
                ColTriangles.Add(ColVertices.Count - 1);
            }
            
        }

        public void AddTriangle(bool add2Render = true, bool add2Col = true)
        {
            if (add2Render)
            {
                Triangles.Add(Vertices.Count - 3);
                Triangles.Add(Vertices.Count - 2);
                Triangles.Add(Vertices.Count - 1);
            }
            if (add2Col)
            {
                ColTriangles.Add(ColVertices.Count - 3);
                ColTriangles.Add(ColVertices.Count - 2);
                ColTriangles.Add(ColVertices.Count - 1);
            }
        }
    }
}
