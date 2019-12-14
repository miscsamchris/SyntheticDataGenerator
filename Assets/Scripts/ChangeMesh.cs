﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeMesh : MonoBehaviour, IChangeable {

    public float range = .2f;

    Mesh originalMesh;

    void Start() {
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
    }

    public void ChangeRandom() {
        Mesh clonedMesh = new Mesh();
        //copy mesh
        clonedMesh.vertices = originalMesh.vertices;
        clonedMesh.triangles = originalMesh.triangles;
        clonedMesh.normals = originalMesh.normals;
        clonedMesh.triangles = originalMesh.triangles;
        clonedMesh.tangents = originalMesh.tangents;
        clonedMesh.uv = originalMesh.uv;
        clonedMesh.uv2 = originalMesh.uv2;
        clonedMesh.uv3 = originalMesh.uv3;
        clonedMesh.uv4 = originalMesh.uv4;
        clonedMesh.uv5 = originalMesh.uv5;
        clonedMesh.uv6 = originalMesh.uv6;
        clonedMesh.uv7 = originalMesh.uv7;
        clonedMesh.uv8 = originalMesh.uv8;

        //get current verts
        List<Vector3> tempVerts = new List<Vector3>();
        clonedMesh.GetVertices(tempVerts);

        List<Vector3> relatedVerts = tempVerts.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

        foreach (Vector3 vert in relatedVerts) {
            Vector3 currVert = vert;
            currVert.x += Random.Range(0f, range);
            currVert.y += Random.Range(0f, range);
            currVert.z += Random.Range(0f, range);

            //replace all similar verts with updated random position
            for (int i = 0; i < tempVerts.Count; i++) {
                if (tempVerts[i] == vert) {
                    tempVerts[i] = currVert;
                }
            }
        }

        clonedMesh.SetVertices(tempVerts);
        GetComponent<MeshFilter>().sharedMesh = clonedMesh;
    }

    void OnApplicationQuit() {
        GetComponent<MeshFilter>().sharedMesh = originalMesh;
    }
}