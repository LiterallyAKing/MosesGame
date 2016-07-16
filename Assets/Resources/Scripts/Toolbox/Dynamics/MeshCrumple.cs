using UnityEngine;
using System.Collections;

[AddComponentMenu("Custom/Dynamics/MeshCrumple")]
public class MeshCrumple : MonoBehaviour {
	public float crumpleScale = 0.5f;
	public float crumpleSpeed = 1f;
	public bool recalculateNormals = true;

	private Vector3[] baseVertices;
	private Perlin noise;

	void Start(){
		noise = new Perlin ();
	}

	void Update () {
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		if (baseVertices == null)
			baseVertices = mesh.vertices;

		Vector3[] vertices = new Vector3[baseVertices.Length];

		float timex = Time.time * crumpleSpeed + 0.1365143f;
		float timey = Time.time * crumpleSpeed + 1.21688f;
		float timez = Time.time * crumpleSpeed + 2.5564f;

		for (int i = 0; i < vertices.Length; i++) {
			Vector3 vertex = baseVertices [i];
			vertex.x += noise.Noise (timex + vertex.x, timex + vertex.y, timex + vertex.z) * crumpleScale;
			vertex.y += noise.Noise (timey + vertex.x, timey + vertex.y, timey + vertex.z) * crumpleScale;
			vertex.z += noise.Noise (timez + vertex.x, timez + vertex.y, timez + vertex.z) * crumpleScale;
			vertices [i] = vertex;
		}

		mesh.vertices = vertices;
		if (recalculateNormals)
			mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
	}
}
