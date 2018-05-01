using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour 
{
	public void NextLevelButton(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
}