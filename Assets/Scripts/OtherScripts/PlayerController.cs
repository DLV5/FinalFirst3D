using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidBody;

    [SerializeField] private float w_speed;
    [SerializeField] private float wb_speed;
    [SerializeField] private float olw_speed;
    [SerializeField] private float rn_speed;
    [SerializeField] private float ro_speed;
    [SerializeField] private Transform _transform;

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            _rigidBody.velocity = transform.forward * w_speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.S))
        {
            _rigidBody.velocity = -transform.forward * wb_speed * Time.deltaTime;
        }
        
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.W))
        {
            _rigidBody.velocity = Vector3.zero;
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            _rigidBody.velocity = Vector3.zero;
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetTrigger("FadeToRun");
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _animator.SetTrigger("FadeToIdle");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _animator.SetTrigger("FadeToRun");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _animator.SetTrigger("FadeToIdle");
        }

        if (Input.GetKey(KeyCode.A))
        {
            _transform.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            _transform.Rotate(0, ro_speed * Time.deltaTime, 0);
        }
    }
}
