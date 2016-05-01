using UnityEngine;
using System.Collections;

public class EnemyEvasiveManeuver : MonoBehaviour 
{

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent <Rigidbody>();
        StartCoroutine (Evade ());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.z);
            yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
        }
    }
    
    void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards (rb.velocity.z, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3 (Random.Range (-5f, -1f), 0.0f, newManeuver);
		//Clip the enemy's movement within the screen.
        rb.position = new Vector3 
        (
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );

		//Make the enemy model tilt when moving.
        rb.rotation = Quaternion.Euler(rb.velocity.z * tilt, 0.0f, 0.0f);
    }
}