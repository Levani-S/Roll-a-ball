using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float speed = 0;
    public Transform pickUpParent;


    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= pickUpParent.childCount)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }
    void OnTriggerEnter (Collider other) 
   {
       if (other.gameObject.CompareTag("PickUp")) 
       {
           other.gameObject.SetActive(false);
           count++;
           SetCountText();
       }
   }
}
