using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleAMoveCtrl : MonoBehaviour {

    [SerializeField]
    private Vector2 followRange =new Vector2(0.8f,0.2f);
    
    [SerializeField]
    private float followSpeed;

    [SerializeField]
    private float castOffset = 1;

    [SerializeField]
    private LayerMask hitLayer;

    [SerializeField]
    private GameObject target;
    [SerializeField]
    private bool followingPlayer = false;

    private bool stunned = false;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if(followingPlayer) FollowTarget();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag(Tag.SQUARE_GROUND) || other.collider.CompareTag(Tag.PLAYER)) StartCoroutine(Stun());
    }

    //Will follow a given target when is in range.
    private void FollowTarget(){
        if (IsTargetInRange()){
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition; //new Vector2(target.transform.position.x, currentPosition.y);
            //Vector2 position = currentPosition;

            if(!stunned){
                targetPosition = new Vector2(target.transform.position.x, currentPosition.y);
            } else {
                //This will push it back when it's stunned
                float targetX = target.transform.position.x > currentPosition.x  ? -target.transform.position.x : target.transform.position.x * 2;
                targetPosition = new Vector2(targetX, currentPosition.y);
            }
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, followSpeed * Time.deltaTime);
        }    
    }

    //Verifies if the target is in range to follow based in a hit layer.
    private bool IsTargetInRange() {
        Vector2 position = new Vector2(transform.position.x, transform.position.y + castOffset);
        RaycastHit2D hit = Physics2D.BoxCast(position, followRange, 0.0f, Vector2.zero,  0.0f, hitLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }

    private IEnumerator Stun(){
        stunned = true;
        yield return new WaitForSeconds(1.5f);
        stunned = false;
    }
    
    private void OnDrawGizmos() {
        Vector2 position = new Vector2(transform.position.x, transform.position.y + castOffset);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(position, followRange);
    }
}
