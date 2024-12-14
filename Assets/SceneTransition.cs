using UnityEngine;

[RequireComponent(typeof(LoadScene))]
public class SceneTransition : MonoBehaviour
{
    public int gameOverScene = 2;
    public void GameOver()
    {
        GetComponent<Animator>().Play("FadeOut");
        Invoke("GoToGameOver", 1f);
    }

    void GoToGameOver() {
        GetComponent<LoadScene>().LoadSceneByIndex(gameOverScene);
    }
}
