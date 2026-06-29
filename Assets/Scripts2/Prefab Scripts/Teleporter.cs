using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] string sceneName;

    [SerializeField] GameObject decal;

    private GameStage gameStage;

    void Awake()
    {
        gameStage = GameObject.Find("GameManager").GetComponent<GameStage>();
        decal = transform.GetChild(0).gameObject;

        if (gameStage.currentGameStage == 0)
        {
            decal.SetActive(false);
        }

        gameStage.GameStageChange += SetActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameStage.currentGameStage != 0)
        {
            if (!other.CompareTag("Player")) return;

            Debug.Log("Changing Scene");

            SceneManager.LoadScene(sceneName);
        }
    }

    void SetActive(int stage, int U)
    {
        if (stage == 0)
        {
            decal.SetActive(false);
        }
        else
        {
            decal.SetActive(true);
        }
    }
}
