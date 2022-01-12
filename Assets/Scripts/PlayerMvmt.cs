using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMvmt : MonoBehaviour
{
    // Player Movement
        public float SPEED = 10.0f;

    // UI:
        private int count = 0;
        public Text count_text;
        public int win_condition;
        public Text win_text;

    private void Start() 
    {
        // Update Texts when Game Starts
        UpdateCountText();
        win_text.text = "";
    }

    private bool grounded = false;

    private void Update() {
        // If Jump Key is pressed and player is on ground:
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            grounded = false;
            // Add force to player in the up direction (jump)
            GetComponent<Rigidbody>().AddForce(new Vector3 (0.0f, 200.0f, 0.0f));
        }
    }

    private void FixedUpdate() 
    {
        /* FixedUpdate is called before performing any physics calculations 
                This is where PHYSICS code will go */

        // Get Movement Inputs
        float move_horizontal = Input.GetAxis("Horizontal") * SPEED;
        float move_vertical   = Input.GetAxis("Vertical")   * SPEED;

        // Make The Player Move:
        GetComponent<Rigidbody>().AddForce(new Vector3(move_horizontal, 0.0f, move_vertical));
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.CompareTag ("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count++;
            UpdateCountText();
        }
    }

    void UpdateCountText() 
    {
        count_text.text = "Count: " + count.ToString();
        if (count >= win_condition) win_text.text = "You Win!";
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) 
        {
            grounded = true;
        }
    }
}
