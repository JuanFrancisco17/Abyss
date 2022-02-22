using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemySettings settings;
    //Puntos entre los que se mueve el enemigo
    [SerializeField]
    private Transform[] waypoints;
    //Siguiente punto al que irá
    private Vector3 nextPosition;
    //Distancia que necesita el enemigo estar para cambiar de waypoint
    [SerializeField]
    float minDistance;
    //Variable que indica en que numero (del waypoint) esta el enemigo 
    private int numberNextPosition = 0;

    void Start()
    {
        nextPosition = waypoints[0].position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, settings.speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPosition) < minDistance)
        {
            numberNextPosition++;

            //Cuando el numero que comprueba en que waypoint esta supera al numbero de waypoints vuelve a 0

            if (numberNextPosition >= waypoints.Length)
            {
                numberNextPosition = 0;
            }
            nextPosition = waypoints[numberNextPosition].position;
        }
    }
}
