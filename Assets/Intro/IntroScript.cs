using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroScript : MonoBehaviour
{
    //Zetilsa raahhhhhhhhhhhh
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
