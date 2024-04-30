using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShieldCtrl : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed =3;
    [SerializeField]
    private float orbit = 1.2f;
    [SerializeField]
    private float duration = 5f;
    [SerializeField]
    private List<GameObject> squareShields = new();

    #region class references
    PlayerDataMgr playerDataMgr;
    #endregion

    #region singleton
    public static ShieldCtrl Instance;
    void Awake() {

        //singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    private void Start() {
        //classes initialization
        playerDataMgr = PlayerDataMgr.GetInstance();
    }

    private void Update() {
        //Rotates the active square shields
        if(squareShields.Count(square => square.activeSelf) >= 1) RotateAroundTarget();
    }

    //Enables damage squares 2 by 2 until reach a maximum of 4 active damage squares.
    public void Launch(){
        //Prevents to execute activate behaviour if all square shields are already active
        if(squareShields.Count(square => square.activeSelf) == 4) return;
        //Verify if the first 2 square shields are already active, if so, activate the rest of the squares
        if(squareShields.Count(square => square.activeSelf) == 2){
            //Activates the second set of the square shields
            Activate(new() { squareShields[1], squareShields[3] });
        }
        //Verify if all square shield are inactive, if so, activate the first 2 squares
        if(squareShields.Count(square => square.activeSelf) == 0){
            //Activates the first set of square shields
            Activate(new() { squareShields[0], squareShields[2] });
        }
    }

    //Will activate the given square shield and apply active behavior
    private void Activate(List<GameObject> squareShields){
        squareShields.ForEach(square => {
            if(!square.activeSelf) {
                square.SetActive(true);
            }
        });
        //Updates the squares amount  
        playerDataMgr.UpdateSquares(-2);
        //Disables enabled squareShields after the given duration time
        StartCoroutine(Deactivate(squareShields));
    }

    //Deactivate the squares after been active.
    private IEnumerator Deactivate(List<GameObject> squareShields){
        yield return new WaitForSeconds(duration - (duration/7));
        squareShields.ForEach(square => {
            //Triggers dissapearing animation
            Animator animator = square.GetComponent<Animator>();
            animator.SetTrigger("Disappearing");
        });
        yield return new WaitForSeconds(duration - (duration/2));
        squareShields.ForEach(square => square.SetActive(false));
    }

    //Rotates all enabled damage square around the target(player).
    private void RotateAroundTarget(){
        //Filter inactive squares
        List<GameObject> activeShields = squareShields.Where(square => square.activeSelf).ToList();
        //Calculate angleOffset depending on the remaining active squares.
        float angleOffset = 360 / activeShields.Count;
        //Apply position calculation to all active squares
        for (int index = 0; index < activeShields.Count; index++){
            //Get current square shield based on index 
            GameObject squareShield = activeShields[index];
            //set pivot position based on a pivot position (player)
            Vector3 pivotPosition = new(transform.position.x, transform.position.y + 0.5f);
            //Calculate the angle with the given values such as time, index, angleOffset and rotation speed.
            float angle = (Time.time + index * angleOffset) * rotationSpeed;
            //Calcuate x and y position based on angle and orbit
            float x = pivotPosition.x + Mathf.Cos(Mathf.Deg2Rad * angle) * orbit;
            float y = pivotPosition.y + Mathf.Sin(Mathf.Deg2Rad * angle) * orbit;
            squareShield.transform.position = new(x, y);
        }
    }
}
