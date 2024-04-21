using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageSquareCtrl : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed =3;
    [SerializeField]
    private float orbit = 1.2f;
    [SerializeField]
    private float angleOffset = 11f;
    [SerializeField]
    private float duration = 5f;
    [SerializeField]
    private List<GameObject> damageSquares = new();

    #region class references
    PlayerDataMgr playerDataMgr;
    #endregion

    #region singleton and initialization
    public static DamageSquareCtrl Instance;
    void Awake() {
        //classes initialization
        playerDataMgr = PlayerDataMgr.GetInstance();

        //singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    private void Update() {
        //Rotate the damage squares that are enabled.
        if(damageSquares.Count(square => square.activeSelf) >= 2) RotateAroundTarget();
    }

    //Enables 2 damage square until have enabled maximum of 4 damage squares.
    public void Enable(){
        //Prevents to execute enable behaviour if all damageSquares are already enabled
        if(damageSquares.Count(square => square.activeSelf) == 4) return;
        //Verify if the first 2 damage squares are already enabled, if so, enable the rest of the squares
        if(damageSquares.Count(square => square.activeSelf) == 2){
            List<GameObject> enabledSquares = new();
            damageSquares.FindAll(square => !square.activeSelf).ForEach(square => {
                if(!square.activeSelf) square.SetActive(true);
                //Add the current enabled squares to a list in order to apply disable behaviour
                enabledSquares.Add(square);
            });
            //Updates the squares amount  
            playerDataMgr.UpdateSquares(-2);
            //Disables enabled damageSquares after the given duration time
            StartCoroutine(Disable(enabledSquares));
        }
        //Verify if all damage squares are disabled, if so, enable the first 2 squares
        if(damageSquares.Count(square => square.activeSelf) == 0){
            List<GameObject> enabledSquares = new();
            damageSquares.GetRange(0,2).ForEach(square => {
                if(!square.activeSelf) square.SetActive(true);
                //Add the current enabled squares to a list in order to apply disable behaviour
                enabledSquares.Add(square);
            });
            //Updates the squares amount  
            playerDataMgr.UpdateSquares(-2);
            //Disables enabled damageSquares after the given duration time
            StartCoroutine(Disable(enabledSquares));
        }
    }

    private IEnumerator Disable(List<GameObject> damageSquares){
        yield return new WaitForSeconds(duration/2);
        damageSquares.ForEach(square => {
            Animator animator = square.GetComponent<Animator>();
            animator.SetTrigger("Disappearing");
        });
        yield return new WaitForSeconds(duration/2);
        damageSquares.ForEach(square => square.SetActive(false));
    }

    private void RotateAroundTarget(GameObject damageSquare, int index, Vector3 pivotPosition){
        float angle = (Time.time + index * angleOffset) * rotationSpeed;
        float x = pivotPosition.x + Mathf.Cos(angle) * orbit;
        float y = pivotPosition.y + 0.5f + Mathf.Sin(angle) * orbit;
        Vector3 targetPosition = new(x, y);
        damageSquare.transform.position = targetPosition;
    }

    //Rotates all enabled damage square arounf the target(player).
    private void RotateAroundTarget(){
        for (int index = 0; index < damageSquares.Count(); index++){
            //Get current damage square based on index 
            GameObject damageSquare = damageSquares[index];
            //set pivot position based on the target position (player)
            Vector3 pivotPosition = new(transform.position.x, transform.position.y + 0.5f);
            //Calculate angle with the given values such as time, index, angleOffset and rotation speed.
            float angle = (Time.time + index * angleOffset) * rotationSpeed;
            //Calcuate x and y position based on angle and orbit
            float x = pivotPosition.x + Mathf.Cos(angle) * orbit;
            float y = pivotPosition.y + Mathf.Sin(angle) * orbit;
            //Set damage square position with the calculated values.
            damageSquare.transform.position = new(x, y);
        }
    }
    
}
