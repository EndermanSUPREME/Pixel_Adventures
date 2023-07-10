using System.Collections;
using UnityEngine;

public class D2_AI_FieldOfView : MonoBehaviour
{
    public float viewRadius;
    // clamp range of values
    [Range(0, 360)] public float viewAngle;
    public LayerMask playerLayer;
    public Transform player, AI;
    [SerializeField] private bool isPlayerHidden;
    private GameObject PlayerModel;
    [SerializeField] Collider2D[] targetsNearby;

    void Start()
    {
        PlayerModel = GameObject.Find("PlayerModel");
        player = PlayerModel.transform;
    }

    void Update()
    {
        isPlayerHidden = player.GetComponent<playerMovement>().hidden;
        FindingTargetsInsideView();
        // see where a ray will point
        Debug.DrawRay(transform.position, -transform.up, Color.red);
    }

    private void FindingTargetsInsideView()
    {
        targetsNearby = Physics2D.OverlapCircleAll(transform.position, viewRadius, playerLayer);

        for (int i = 0; i < targetsNearby.Length; i++)
        {
            Transform target = targetsNearby[i].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized; // scales from 0-1

            // takes an angle between a ray that shoots straight forward from its position and a ray that points from itself to the target checking if its inside the bounded view angle
            if (Vector3.Angle(-transform.up, directionToTarget) < viewAngle/2)
            {
                //print("Inside view angle");
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                RaycastHit2D AI_View = Physics2D.Raycast(transform.position, directionToTarget, dstToTarget, playerLayer);
				if (AI_View)
				{
                    //print("AI raycast Hit");
                    // if when we shoot a raycast at a player and we dont hit an obstacle AKA can't see through the walls
					if (isPlayerHidden == false)
                    {
                        if (AI.GetComponent<RockMonsterAI>() != null)
                        {
                            AI.GetComponent<RockMonsterAI>().Detect();
                        }

                        if (AI.GetComponent<CaveBeatleScript>() != null)
                        {
                            AI.GetComponent<CaveBeatleScript>().Detect();
                        }

                        if (AI.GetComponent<AI_Script>() != null)
                        {
                            AI.GetComponent<AI_Script>().Detect();
                        }
                    }
				}
            }
        }
    }
    
    public Vector3 DirectionFromAngle(float angleInDegrees, bool isTheAngleGlobal)
    {
        if (isTheAngleGlobal != true)
        {
            // 2D objects rotate around the Z-Axis
            angleInDegrees += transform.eulerAngles.z;
        };
        // the vector values correlate to the special triangles trig values
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}//EndScript