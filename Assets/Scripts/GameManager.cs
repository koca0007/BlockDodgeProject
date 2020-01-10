using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public float slow = 10f;


    public void RestartLevel()
    {


        Time.timeScale = 0f;
        Time.fixedDeltaTime *= slow;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        FindObjectOfType<Score>().RestartGame();

    }

 
}
