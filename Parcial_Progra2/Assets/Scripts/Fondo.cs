using UnityEngine;

public class Fondo : MonoBehaviour
{
    public float velocidad = 2f;
    public Renderer imagenFondo;

    void Update()
    {
        float offset = Time.time * velocidad;
        imagenFondo.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
