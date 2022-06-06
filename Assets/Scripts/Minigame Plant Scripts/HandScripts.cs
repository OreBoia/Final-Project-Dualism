using UnityEngine;

public class HandScripts : IMovement
{
    [SerializeField]
    Rigidbody2D leafObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LeafScripts leaf = collision.gameObject.GetComponent<LeafScripts>();

        if (leaf != null)
        {
            leafObj = leaf.gameObject.GetComponent<Rigidbody2D>();
            //leaf.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        leafObj = null;
    }

    private void OnTear()
    {
        Debug.Log("PICK");
        if (leafObj != null)
        {
            leafObj.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnRelease()
    {
        Debug.Log("RELEASE");
    }
}
