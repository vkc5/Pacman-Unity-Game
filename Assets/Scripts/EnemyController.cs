using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum GhostNOdeStatesEnum
    {
        resopwning,
        leftNode,
        rigthNode,
        centerNode,
        startNode,
        movingInNodes
    }

    public GhostNOdeStatesEnum ghostNOdeStates;

    public enum GhostcolorEnum
    {
        red,
        pink,
        blue,
        orange
    }

    public GhostcolorEnum ghostcolor;


    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;
    public GameObject ghostNodeStart;

    public MovementController movementController;

    public GameObject startingNode;

    public bool ReadyToLeaveHome = false;

    public GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        movementController = GetComponent<MovementController>();
        if (ghostcolor == GhostcolorEnum.red)
        {
            ghostNOdeStates = GhostNOdeStatesEnum.startNode;
            startingNode = ghostNodeStart;
        }
        else if (ghostcolor == GhostcolorEnum.pink)
        {
            ghostNOdeStates = GhostNOdeStatesEnum.leftNode;
            startingNode = ghostNodeLeft;
        }
        else if (ghostcolor == GhostcolorEnum.blue)
        {
            ghostNOdeStates = GhostNOdeStatesEnum.rigthNode;
                startingNode = ghostNodeRight;
        }
        else if (ghostcolor == GhostcolorEnum.orange)
        {
            ghostNOdeStates = GhostNOdeStatesEnum.centerNode;
            startingNode = ghostNodeCenter;

        }
        movementController.currentnode = startingNode;
        transform.position = startingNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReachedCenterOfNOde (NodeController node)
    {
        if (ghostNOdeStates == GhostNOdeStatesEnum.movingInNodes)
        {
            if (ghostcolor == GhostcolorEnum.red)
            {
                DerterminRedGhostdirection();
            }
            else if (ghostcolor == GhostcolorEnum.pink)
            {
                DerterminRedGhostdirection();

            }
            else if (ghostcolor == GhostcolorEnum.blue)
            {
                DerterminRedGhostdirection();

            }
            else if (ghostcolor == GhostcolorEnum.orange)
            {
                DerterminRedGhostdirection();
            }
        }
        else if (ghostNOdeStates == GhostNOdeStatesEnum.resopwning)
        {

        }
        else
        {
            if (ReadyToLeaveHome)
            {
                if (ghostNOdeStates == GhostNOdeStatesEnum.rigthNode)
                {
                    ghostNOdeStates = GhostNOdeStatesEnum.centerNode;
                    movementController.SetDirection("left");
                }
                else if (ghostNOdeStates == GhostNOdeStatesEnum.leftNode)
                {
                    ghostNOdeStates = GhostNOdeStatesEnum.centerNode;
                    movementController.SetDirection("rigth");
                }
                else if (ghostNOdeStates == GhostNOdeStatesEnum.centerNode)
                {
                    ghostNOdeStates = GhostNOdeStatesEnum.startNode;
                    movementController.SetDirection("up");
                }
                else if (ghostNOdeStates == GhostNOdeStatesEnum.startNode)
                {
                    ghostNOdeStates = GhostNOdeStatesEnum.movingInNodes;
                    movementController.SetDirection("left");
                }  


            }
        }
         void DerterminRedGhostdirection () { 
        
            string direction = GetClosestDirection(gameManager.pacman.transform.position);
            movementController.SetDirection(direction);
        }

        string GetClosestDirection(Vector2 target)
        {
            float shortestDistance = 0;
            string lastmovingDirection = movementController.lastDirection;
            string newDirection = "";
            NodeController nodeController = movementController.currentnode.GetComponent<NodeController>();

            if (nodeController.canMoveUp && lastmovingDirection != "down")
            {
                GameObject nodeup = nodeController.upNode;
                float distance = Vector2.Distance(nodeup.transform.position, target);

                if (shortestDistance == 0 || distance < shortestDistance)
                {
                    shortestDistance = distance;
                    newDirection = "up";
                }
            }
            if (nodeController.canMoveDown && lastmovingDirection != "up")
            {
                GameObject nodedown = nodeController.downNode;
                float distance = Vector2.Distance(nodedown.transform.position, target);
                if (shortestDistance == 0 || distance < shortestDistance)
                {
                    shortestDistance = distance;
                    newDirection = "down";
                }
            }
            if (nodeController.canMoveLeft && lastmovingDirection != "rigth")
            {
                GameObject nodeleft = nodeController.leftNode;
                float distance = Vector2.Distance(nodeleft.transform.position, target);
                if (shortestDistance == 0 || distance < shortestDistance)
                {
                    shortestDistance = distance;
                    newDirection = "left";
                }
            }
            if (nodeController.canMoveRigth && lastmovingDirection != "left")
            {
                GameObject noderigth = nodeController.rigthNode;
                float distance = Vector2.Distance(noderigth.transform.position, target);
                if (shortestDistance == 0 || distance < shortestDistance)
                {
                    shortestDistance = distance;
                    newDirection = "rigth";
                }
            }
            return newDirection;
        }


    }
}
