using UnityEngine;

public class CameraCtrl : MonoBehaviour {
    
    [SerializeField]
    private GameObject target; // Target to be followed

    [SerializeField]
    private Vector3 offset; // Offset from the target position

    [SerializeField]
    private Transform topLimit;

    [SerializeField]
    private Transform botLimit;

    [SerializeField]
    private float speed = 5f; // Speed of camera movement

    [SerializeField]
    private float smoothSpeed = 2f;

    [SerializeField]
    private float lookOffset;

    private Vector3 targetPosition;
    private Rigidbody2D targetRigidBody;

    private void Start() {
        //Set initial positions for camera following
        targetPosition = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, transform.position.z);
        transform.position = targetPosition;
        targetRigidBody = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate(){
        //Follows the target
        FollowTarget();
    }
    
    //Sets x offset depending on facing direction
    private void SetHorizontalOffset(){
        float facingDirection = Input.GetAxisRaw("Horizontal");
        //Set offset depending on target facing direction.
        if(facingDirection > 0) lookOffset = Mathf.Lerp(lookOffset, offset.x, Time.deltaTime * smoothSpeed);
        if(facingDirection < 0) lookOffset = Mathf.Lerp(lookOffset, -offset.x, Time.deltaTime * smoothSpeed);
    }

    //Will follow a target depending on its behavior
    private void FollowTarget(){
        if (target != null) {
            //Calculates the horizontal offet to apply in x position
            SetHorizontalOffset();
            //Set target X position according to the given target position
            targetPosition.x = target.transform.position.x + lookOffset;
            //Verifies if camera can update Y position
            if(IsTargetOutOfLimit() && (IsTargetGrounded() || IsTargetFalling())){
                targetPosition.y = target.transform.position.y + offset.y;
            }
            //Set target position to the camera position applying movement speed
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }

    //Verifies if target is grounded
    private bool IsTargetGrounded(){
        Vector2 groundRange = new(0.96f, 0.1f);
        float groundCastOffset = 0.06f;
        Vector2 position = new(target.transform.position.x, target.transform.position.y - groundCastOffset);
        return CastHelper.IsWithin2DBox(position, groundRange, Vector2.down, Tag.GROUNDED_TAGS);
    }

    private bool IsTargetOutOfLimit() => IsTargetInTop() || IsTargetInBot();

    private bool IsTargetInTop() => target.transform.position.y >= topLimit.position.y;

    private bool IsTargetInBot() => target.transform.position.y <= botLimit.position.y;

    private bool IsTargetFalling() => targetRigidBody.velocity.y < 0 && !IsTargetGrounded() && IsTargetInBot();
}
