using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallBehavior : MonoBehaviour
{
    //Speed information
    public float speed = 5;
    public float rotateSpeed = 2000;
    public bool reverse;

    //Waypoint information
    [SerializeField]
    Transform[] WayPoints;
    public int currentWP;

    public bool directionUp = true;

    // Start is called before the first frame update
    void Start()
    {
        //Start at the correct waypoint
        if (WayPoints != null && WayPoints.Length > 0)
        {
            transform.position = WayPoints[currentWP].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.smoothDeltaTime);

        FollowWP();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            directionUp = !directionUp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            directionUp = !directionUp;
            return;
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            directionUp = !directionUp;
        }
    }

    void FollowWP()
    {
        if (WayPoints == null || WayPoints.Length == 0)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position,
        WayPoints[currentWP].transform.position, speed * Time.deltaTime);

        if (transform.position == WayPoints[currentWP].transform.position)
        {
            if (reverse) currentWP -= 1;
            else currentWP += 1;
        }

        if (currentWP >= WayPoints.Length) currentWP = 0;
        if (currentWP < 0) currentWP = WayPoints.Length - 1;
    }
}
