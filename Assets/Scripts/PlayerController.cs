using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;
    public KeyCode toggleSpiritKey;
    public KeyCode lanternKey;
    public Animator animator;
    public GameObject happyEnding;
    public GameObject sadEnding;
    public GameObject gameOver;

    private Rigidbody rigidBody;

    private int collectibles;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed * Time.deltaTime;
        animator.SetFloat("MoveSpeed", movement.magnitude);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        rigidBody.MovePosition(transform.position + movement);


        if (Input.GetKeyDown(toggleSpiritKey))
        {
            GameManager.Instance.ToggleLimbo();
        }

        if (Input.GetKeyDown(lanternKey))
        {
            animator.SetTrigger("LanternFlash");
        }
    }

    public void CollectibleFound()
    {
        collectibles++;
    }

    public void FoundCottage()
    {
        if(collectibles > 3)
        {
            happyEnding.SetActive(true);
        }
        else
        {
            sadEnding.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }
}
