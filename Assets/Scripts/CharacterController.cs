using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 5f,range=15;
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Rigidbody hip;

    [SerializeField] private Animator targetAnimator;
    [SerializeField] Transform player;

    private bool walk = false;

    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;
    ConfigurableJoint[] configurableJoints;
    public PlayerController playerController;
    public LevelManager levelManager;
    bool isDead;

    void Start()
    {
        isDead = false;
        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        configurableJoints = GetComponentsInChildren<ConfigurableJoint>();
    }

    public void OnAIDeath()
    {

        foreach (ConfigurableJoint cj in configurableJoints)
        {
            if (cj.transform.name == "Hip")
            {
                JointDrive angular = new JointDrive();
                angular.positionSpring = 100;
                angular.positionDamper = 0;
                cj.angularXDrive = angular;
                angular.positionSpring = 800;
                angular.positionDamper = 0;
                cj.angularYZDrive = angular;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
        }
        
    }

    void Move()
    {
      
        Vector3 direction = (player.position - hip.transform.position);
        //this.hip.MovePosition(player.position);
        //this.walk = true;

        if (direction.magnitude >= 0.5f && direction.magnitude < range)
        {
            //float targetAngle = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;

           //  this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            hip.transform.LookAt(player);
            hip.transform.position += hip.transform.forward * speed * Time.deltaTime;
            this.hip.AddForce(hip.transform.forward * speed, ForceMode.Force);

            this.walk = true;
        }
        else
        {
            this.walk = false;
        }

        this.targetAnimator.SetBool("Walk", this.walk);
    }

    public void Collapse()
    {
        isDead = true;
        this.targetAnimator.SetBool("Walk",false);
        levelManager.ActiveEnemies.Remove(this.gameObject);
        levelManager.DeadEnemies.Add(this.gameObject);
        OnAIDeath();
        playerController.GetNearestEnemy();
        playerController.NextPath();
    }
}
