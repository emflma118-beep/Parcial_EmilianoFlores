using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TextMeshProUGUI texto;
    public AudioClip sonido;

    private int score = 0;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
        audioSource.PlayOneShot(sonido);
    }

    void UpdateScoreUI()
    {
        if (texto != null)
        {
            texto.text = "Puntos: " + score;
        }
    }
}
