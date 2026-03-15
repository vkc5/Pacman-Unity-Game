using UnityEngine;

public class NodeSeleter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.tag == "Node")
        {
            Destroy(Collision.gameObject);
        }
    }
}
