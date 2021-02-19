using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GrassNormals : MonoBehaviour
{
    [SerializeField] Color[] topColors;
    // Start is called before the first frame update
    void Start()
    {
        //Some variables to change normals. Didn't look good. Maybe needs some adjustment

        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //Vector3[] vertices = mesh.vertices;
        //Color[] colors = mesh.colors;
        //Vector3[] normals = mesh.normals;

        LayerMask layerMask = ~LayerMask.NameToLayer("Environment");
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            this.gameObject.transform.position = hit.point;

            //for (int i = 0; i < vertices.Length; i++)
            //{
            //    float red = colors[i].g;
            //    if (red < 0.5f)
            //    {
            //        normals[i] = hit.normal;
                    
            //    }

            //}
            //mesh.normals = normals;
        }
        int q = Random.Range(0, topColors.Length);
        this.GetComponent<Renderer>().material.SetColor("_Color_2", topColors[q]);
        Destroy(this);
    }

    
}
