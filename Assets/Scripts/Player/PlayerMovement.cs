using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movement;
    public Vector2 priorityDirection = Vector2.zero;
    public Vector2 lastDirection = Vector2.down;
    void Start()
    {
        if (PlayerGameData.hasSavedPosition)
        {
            transform.position = PlayerGameData.playerPosition;
        }
        EntranceTrigger[] entrances = FindObjectsOfType<EntranceTrigger>();
        if (PlayerGameData.isInsideBuilding)
        {
            foreach (EntranceTrigger entrance in entrances)
            {
                if (entrance.buildingName == PlayerGameData.currentBuilding)
                {
                    entrance.SetBuildingCamera();
                }
            }
        }
    }
    void Update()
    {
        // 처음 누른 방향 우선으로 선택
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && priorityDirection == Vector2.zero)
        {
            priorityDirection = Vector2.up;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && priorityDirection == Vector2.zero)
        {
            priorityDirection = Vector2.down;
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && priorityDirection == Vector2.zero)
        {
            priorityDirection = Vector2.left;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && priorityDirection == Vector2.zero)
        {
            priorityDirection = Vector2.right;
        }

        //W가 우선일때 W를 놓은 경우
        if((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && priorityDirection == Vector2.up)
        {
            priorityDirection = Vector2.zero;

            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                priorityDirection = Vector2.right;
            }
            else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                priorityDirection = Vector2.left;
            }
            else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                priorityDirection = Vector2.down;
            }
        }
        //S가 우선일때 S를 놓은 경우
        if((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) && priorityDirection == Vector2.down)
        {
            priorityDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                priorityDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                priorityDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                priorityDirection = Vector2.right;
            }
        }
        //A가 우선일때 A를 놓는 경우
        if((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && priorityDirection == Vector2.left)
        {
            priorityDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                priorityDirection = Vector2.right;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                priorityDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                priorityDirection = Vector2.down;
            }
        }
        //D가 우선일때 D를 놓는 경우
        if((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && priorityDirection == Vector2.right)
        {
            priorityDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                priorityDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                priorityDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                priorityDirection = Vector2.down;
            }
        }

        movement = priorityDirection; //실제 이동방향 결정
        if (movement != Vector2.zero)
        {
            lastDirection = movement;
        }
        bool isWalking = movement != Vector2.zero;
        animator.SetBool("isWalking", isWalking);

        if (isWalking == true)
        {
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(
            rb.position + movement * moveSpeed * Time.fixedDeltaTime
        );
    }
}