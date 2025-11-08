using UnityEngine;

public class Fondo : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public Renderer backgroundRenderer;

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        backgroundRenderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
