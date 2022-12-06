using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int potatoCount;
    public static int currentCount;
    bool visible;

    void Start()
    {
        //At the start, the flag will be invisible
        currentCount = 0;
        visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCount >= potatoCount){
            visible = true;
        }
        Color c = this.GetComponent<SpriteRenderer>().color;
        if(!visible){
            //make alpha 0
            c.a = 0;
        }
        else if(visible){   
            c.a = 1;
        }
        this.GetComponent<SpriteRenderer>().color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && visible)
            {
                LoadNextScene();
            }
        }

         public void LoadNextScene()
    {
        var nextScene = "Level_1";

        var currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName.StartsWith("Level"))
        {
            var values = currentSceneName.Split("_");

            if (values.Length < 2)
            {
                return;
            }

            nextScene = $"Level_{int.Parse(values[1]) + 1}";
        }

        Debug.Log($"Current Scene: {currentSceneName}. Next Scene: {nextScene}");

        SceneManager.LoadScene(nextScene);

        /*
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex){
            SceneManager.LoadScene(nextSceneIndex);
        }
        */
    }
}
