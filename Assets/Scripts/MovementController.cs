using UnityEngine;
using UnityEngine.Rendering;

public class MovementController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameManager gameManager;

    public GameObject currentnode;
    public float speed = 3f;

    public string direction = "";
    public string lastDirection = "";

    public bool canWarp = true;

    public bool isGhost = false;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (currentnode != null)
        {
            transform.position = currentnode.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        NodeController currentNodecController = currentnode.GetComponent<NodeController>();

        transform.position = Vector3.MoveTowards(transform.position, currentnode.transform.position, speed * Time.deltaTime);

        bool reverse = false;
        if (direction == "left" && !currentNodecController.canMoveLeft)
        {
            reverse = true;
        }
        else if (direction == "rigth" && !currentNodecController.canMoveRigth)
        {
            reverse = true;
        }
        else if (direction == "up" && !currentNodecController.canMoveUp)
        {
            reverse = true;
        }
        else if (direction == "down" && !currentNodecController.canMoveDown)
        {
            reverse = true;
        }

        if (Vector2.Distance(transform.position, currentnode.transform.position) < 0.05f || reverse)
        {
            if (isGhost)
            {
                GetComponent<EnemyController>().ReachedCenterOfNOde(currentNodecController);
            }

            if (currentNodecController.isWarpLeftNode && canWarp)
            {
                currentnode = gameManager.rightWarpNode;
                direction = "left";
                lastDirection = "left";
                transform.position = currentnode.transform.position;
                canWarp = false;
            }
            else if (currentNodecController.isWarpRightNode && canWarp)
            {
                currentnode = gameManager.leftWarpNode;
                direction = "rigth";
                lastDirection = "rigth";
                transform.position = currentnode.transform.position;
                canWarp = false;
            }

                transform.position = currentnode.transform.position;

            GameObject newNode = currentNodecController.GetNode(direction);
            if (newNode != null)
            {
                currentnode = newNode;
                lastDirection = direction;
            }
            else
            {
                direction = lastDirection;
                newNode = currentNodecController.GetNode(direction);
                if (newNode != null)
                {
                    currentnode = newNode;
                }
            }
        }
        else
        {
            canWarp = true;
        }

    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
}
