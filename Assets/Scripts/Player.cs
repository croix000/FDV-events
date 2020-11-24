using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] Transform movePoint;
    Transform sprite;
    LayerMask obstacleMask;
    float flipX;
    bool isMoving;

    float horizontal = 0f;
    float vertical = 0f;
    Vector3 lastDirection;

    Mover mover;

    public delegate void CoinCollected();
    public event CoinCollected onCoinCollected;
    public delegate void DiamondCollected();
    public event DiamondCollected onDiamondCollected;
    void Start()
    {

        sprite = GetComponentInChildren<SpriteRenderer>().transform;
        flipX = sprite.localScale.x;
        obstacleMask = LayerMask.GetMask("Wall");
        movePoint.parent = null;
        mover = GetComponent<Mover>();
    }


    private void Update()
    {
      

        horizontal = Mathf.Clamp(Input.GetAxisRaw("Horizontal") , -1, 1);
        vertical = Mathf.Clamp(Input.GetAxisRaw("Vertical") , -1, 1);


        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f && !isMoving)
        {
            Vector3 hitSize = Vector3.one * 0.5f;
            if (Mathf.Abs(horizontal) == 1)
            {

                if (!Physics2D.OverlapBox(movePoint.position + new Vector3(horizontal, 0f, 0f), hitSize, 0, obstacleMask))
                {
                    isMoving = true;
                    movePoint.position += new Vector3(horizontal, 0f, 0f);
                    sprite.localScale = new Vector2(flipX * horizontal, sprite.localScale.y);
                    SetLastDirection(new Vector3(horizontal, 0f, 0f));
                    mover.MoveTo(movePoint.position);
                }
            }
            else if (Mathf.Abs(vertical) == 1)
            {
                if (!Physics2D.OverlapBox(movePoint.position + new Vector3(0f, vertical, 0f), hitSize, 0, obstacleMask))
                {
                    isMoving = true;
                    movePoint.position += new Vector3(0f, vertical, 0f);
                    SetLastDirection(new Vector3(0f, vertical, 0f));
                    mover.MoveTo(movePoint.position);
                }
            }


        }
        if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) == 0)
        {
            isMoving = false;

        }
        //}
    }

    public void SetLastDirection(Vector3 dir)
    {
        this.lastDirection = dir;

    }
    public Vector3 GetLastDirection()
    {
        return lastDirection;
    }


    public void CoinCollision() {

        onCoinCollected();
    }

    public void DiamondCollision()
    {

        onDiamondCollected();
    }

}

