using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    private Material firstMaterial;
    private Material secondMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFirstMaterial(Material mat, string texturePath)
    {
        firstMaterial = mat;
        firstMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
    }

    public void SetSecondMaterial(Material mat, string texturePath)
    {
        secondMaterial = mat;
        secondMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
    }

    public void ApplyFirstMaterial()
    {
        gameObject.GetComponent<Renderer>().material = firstMaterial;
    }

    public void ApplySecondMaterial()
    {
        gameObject.GetComponent<Renderer>().material = secondMaterial;
    }
}
