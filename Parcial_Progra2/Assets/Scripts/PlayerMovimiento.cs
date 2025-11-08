using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovimiento : MonoBehaviour
{
    [Header("Configuración del Jugador")]
    public float fuerzaSalto = 7f;
    public float alturaAgacharse = 0.5f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isCrouching = false;
    private float altura;
    private Vector3 centro;

    [Header("Sonidos")]
    public AudioClip salto;
    public AudioClip agacharse;
    public AudioClip muerte;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        altura = transform.localScale.y;
        centro = GetComponent<BoxCollider>().center;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && isGrounded && !isCrouching)
        {
            Salto();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            Agacharse();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && isCrouching)
        {
            StandUp();
        }
    }

    void Salto()
    {
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        isGrounded = false;
        audioSource.PlayOneShot(salto);
    }

    void Agacharse()
    {
        isCrouching = true;
        // Reducir altura del collider
        transform.localScale = new Vector3(transform.localScale.x, alturaAgacharse, transform.localScale.z);
        GetComponent<BoxCollider>().center = new Vector3(centro.x, centro.y - (altura - alturaAgacharse) / 2, centro.z);
        audioSource.PlayOneShot(agacharse);
    }

    void StandUp()
    {
        isCrouching = false;
        // Restaurar altura original
        transform.localScale = new Vector3(transform.localScale.x, altura, transform.localScale.z);
        GetComponent<BoxCollider>().center = centro;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            // Muerte y recarga de escena
            audioSource.PlayOneShot(muerte);
            Invoke("ReiniciarEscena", 1f);
        }
    }

    void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
