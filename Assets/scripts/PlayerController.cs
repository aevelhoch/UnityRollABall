using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float jumpPower;
    public int numJumps;
    private int currentJumps;
    private bool jumpKeyUp;

    private int count;
    public Text countText;
    public Text winText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
        currentJumps = numJumps;
        jumpKeyUp = true;
    }

    void FixedUpdate()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHoriz, 0.0f, moveVert);

        if (Input.GetButtonDown("Jump") && currentJumps > 0 && jumpKeyUp == true)
        {
            movement = movement + new Vector3(0.0f, jumpPower, 0.0f);
            currentJumps--;
            jumpKeyUp = false;
        }
        if (Input.GetButtonUp("Jump")) jumpKeyUp = true;

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        currentJumps = numJumps;
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 13) winText.text = "You Win!";
    }
}