using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private float creditsDuration = 25f;

    private void Start()
    {
        Invoke("ReturnToMenu", creditsDuration);
    }

    private void Update()
    {
        // Tekan SPACEBAR = balik cepet
        if (Input.GetKeyDown(KeyCode.Space))
            ReturnToMenu();
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}