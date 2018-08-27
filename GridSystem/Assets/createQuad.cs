using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class createQuad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3[] vertices = new Vector3[3];
        int[] triangles = new int[3];
        MeshFilter mf = GetComponent<MeshFilter>();
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 1, 0);
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        mf.mesh.vertices = vertices;
        mf.mesh.triangles = triangles;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
