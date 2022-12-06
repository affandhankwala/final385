using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    Node[] PathNode;
    public GameObject Obstacle;
    public float MoveSpeed;
    float Timer;
    static Vector3 CurrentPositionHolder;
    int CurrentNode;

    void Start(){
        PathNode = GetComponentsInChildren<Node>();
        CheckNode();
    }

    void CheckNode(){
        Timer = 0;
        CurrentPositionHolder = PathNode[CurrentNode].transform.position;
    }

    void Update(){
        Timer += Time.deltaTime * MoveSpeed;
        if(Obstacle.transform.position != CurrentPositionHolder){
            Obstacle.transform.position = Vector3.Lerp(Obstacle.transform.position, CurrentPositionHolder, Timer);
        }
        else{
            if (CurrentNode < PathNode.Length - 1){
                CurrentNode++;
                CheckNode();
            }
        }
    }
}

