using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerBehavior : MonoBehaviour
{

    public float speed = 10f;
    private float rotationSpeed = 1500f;
    public bool gameOver = false;
    static private int currentLevelDeaths = 0;
    public static int deathCount;
    public Text counterText;
   

    [SerializeField]
    private GameObject Oubliette;
    [SerializeField]
    private GameObject flagSample;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);

        counterText.text = "Death Count: " + deathCount.ToString();

        if (movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                deathCount++;
                RestartScene();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"Collision Object: Name={collision.name} Tag={collision.tag}");

        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "SpikeBall")
        {
            TriggerGameOver();
        }
        else if (collision.gameObject.tag == "Pickup")
        {
            speed *= 0.7f;
            Destroy(collision.gameObject);
            FlagSpawn.currentCount++;
        }
        
    }

    public void TriggerGameOver()
    {
        speed = 0f;
        rotationSpeed = 0f;
        Oubliette.SetActive(true);
        gameOver = true;
    }

    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

}