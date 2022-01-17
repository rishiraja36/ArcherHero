using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float force;
    public GameObject bulletPrefab;
    public Transform gunEnd,bow;
    private Vector3 aim;
    public LevelManager levelManager;
    public GameObject currentTargetEnemy;

    public PathCreator[] pathCreators;

    public PathFollower pathFollower;

    void Start()
    {
        GetNearestEnemy();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (currentTargetEnemy != null)
        {
            Quaternion lookOnLook = Quaternion.LookRotation(currentTargetEnemy.transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);

            //transform.LookAt(currentTargetEnemy.transform);
        }
    }

    void Shoot()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePoint);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 5);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            aim = hit.point;
            Debug.Log(hit.point);
        }
        else
        {
            aim = ray.GetPoint(10);
        }

        Vector3 aimDirection = aim - gunEnd.position;

        GameObject arrow = Instantiate(bulletPrefab, gunEnd.position, Quaternion.LookRotation(aimDirection));
        arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * force;
        bow.transform.rotation = Quaternion.LookRotation(aimDirection);

    }

    public void GetNearestEnemy()
    {
        if(levelManager.ActiveEnemies.Count>0)
        {

            levelManager.ActiveEnemies = levelManager.ActiveEnemies.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)
            ).ToList();

            currentTargetEnemy = levelManager.ActiveEnemies[0];
            
        }
      else
        {
            currentTargetEnemy = null;
        }
    }

    public void NextPath()
    {
        pathFollower.pathCreator = pathCreators[levelManager.DeadEnemies.Count];
        pathFollower.distancePercentage = 0;
        pathFollower.distanceTravelled = 0;
    }

}
