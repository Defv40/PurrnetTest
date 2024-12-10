using PurrNet;
using UnityEngine;

public class CharacterManager : NetworkBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    protected override void OnSpawned(bool asServer)
    {
        enabled = isOwner;
    }

    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector2 input = GetInput();
        Vector3 dir = -transform.forward * input.y + -transform.right * input.x;
        dir.Normalize();
        dir *= movementSpeed;
        dir.y = Physics.gravity.y;
        
        _rb.linearVelocity = dir;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }

        
    }
  

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
