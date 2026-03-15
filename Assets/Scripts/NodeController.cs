using UnityEngine;

public class NodeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool canMoveLeft = false;
    public bool canMoveRigth = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject leftNode;
    public GameObject rigthNode;
    public GameObject upNode;
    public GameObject downNode;

    public bool isWarpRightNode = false;
    public bool isWarpLeftNode = false;

    public bool isPelletNode = false;

    public bool hasPellet = false;


    public SpriteRenderer PelletSprite;

    public GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (transform.childCount > 0)
        {
            isPelletNode = true;
            hasPellet = true;
            PelletSprite = GetComponentInChildren<SpriteRenderer>();
        }


        RaycastHit2D[] hitDown;
        hitDown = Physics2D.RaycastAll(transform.position, -Vector2.up);
        for (int i = 0; i < hitDown.Length; i++)
        {
            float distance = Mathf.Abs(hitDown[i].point.y - transform.position.y);
            if (distance < 0.4f && hitDown[i].collider.tag == "Node")
            {
                canMoveDown = true;
                downNode = hitDown[i].collider.gameObject;
            }
        }
        RaycastHit2D[] hitUp;
        hitUp = Physics2D.RaycastAll(transform.position, Vector2.up);
        for (int i = 0; i < hitUp.Length; i++)
        {
            float distance = Mathf.Abs(hitUp[i].point.y - transform.position.y);
            if (distance < 0.4f && hitUp[i].collider.tag == "Node")
            {
                canMoveUp = true;
                upNode = hitUp[i].collider.gameObject;
            }
        }
        RaycastHit2D[] hitLeft;
        hitLeft = Physics2D.RaycastAll(transform.position, -Vector2.right);
        for (int i = 0; i < hitLeft.Length; i++)
        {
            float distance = Mathf.Abs(hitLeft[i].point.x - transform.position.x);
            if (distance < 0.4f && hitLeft[i].collider.tag == "Node")
            {
                canMoveLeft = true;
                leftNode = hitLeft[i].collider.gameObject;
            }
        }
        RaycastHit2D[] hitRigth;
        hitRigth = Physics2D.RaycastAll(transform.position, Vector2.right);
        for (int i = 0; i < hitRigth.Length; i++)
        {
            float distance = Mathf.Abs(hitRigth[i].point.x - transform.position.x);
            if (distance < 0.4f && hitRigth[i].collider.tag == "Node")
            {
                canMoveRigth = true;
                rigthNode = hitRigth[i].collider.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
        {

        }

    public GameObject GetNode(string direction)
    {
        if (direction == "up" && canMoveUp)
        {
            return upNode;
        }
        else if (direction == "down" && canMoveDown)
        {
            return downNode;
        }
        else if (direction == "left" && canMoveLeft)
        {
            return leftNode;
        }
        else if (direction == "rigth" && canMoveRigth)
        {
            return rigthNode;
        }
        else
        {
            return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPelletNode && hasPellet && collision.tag == "Player")
        {
            hasPellet = false;
            PelletSprite.enabled = false;
            gameManager.CollectedPellet(this);
        }
    }
}
