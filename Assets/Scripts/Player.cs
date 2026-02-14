using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _playerSpeed=10f;
    [SerializeField]
    private float _playerRotateSpeed=10f;
    [SerializeField]
    float mouseSensitivity = 100f;
    [SerializeField] 
    private Transform cameraPivot;
    [SerializeField] 
    private Animator _gunAnimator;
    [SerializeField]
    private AudioSource _gunFireSound;
    [SerializeField]
    private float _fireRate = 0.5f;


    private float _nextFireTime = 0f;

    Vector2 input;
    Vector2 lookInput;

    private float xRotation = 0f;

    public InputActionReference move;
    public InputActionReference lookAction;
    public InputActionReference fireAction;

    void OnEnable()
    {
        move.action.Enable();
        lookAction.action.Enable();
        fireAction.action.Enable();

    }

    void OnDisable()
    {
        move.action.Disable();
        lookAction.action.Disable();
        fireAction.action.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //look Around
        lookInput = lookAction.action.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //movement
        input = move.action.ReadValue<Vector2>().normalized;
        Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;


        transform.position += moveDirection * _playerSpeed * Time.deltaTime;

        if (fireAction.action.triggered && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void Shoot()
    {
        Debug.Log("Bang!");
        _gunAnimator.SetTrigger("Fire");
        _gunFireSound.Play();

    }


}
