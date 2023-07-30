using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    InputAction mousePosition;
    InputAction fire;

    [SerializeField] bool isFiring = false;

    public GameObject playerDirectionalLight;
    public GameObject fireBallPrefab;
    public float bulletForce = 10.0f;
    public float coolDownInterval = 0.2f;

    bool isCoolingDown = false;

    public AudioSource audio;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }
    private void OnEnable() {
        mousePosition = playerInputActions.Player.MousePosition;
        mousePosition.Enable();

        fire = playerInputActions.Player.Fire;
        fire.Enable();
        fire.performed += (InputAction.CallbackContext ctx) => {isFiring = true;};
        fire.canceled += (InputAction.CallbackContext ctx) => {isFiring = false;};
    }
    private void OnDisable() {
        mousePosition.Disable();
        fire.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse_screen_pos = mousePosition.ReadValue<Vector2>();
        Vector3 mouse_world_pos = Camera.main.ScreenToWorldPoint(new Vector3(mouse_screen_pos.x, mouse_screen_pos.y, Camera.main.nearClipPlane));
        Vector3 tmp = mouse_world_pos - transform.position;
        Vector3 player2mouse = new Vector2(tmp.x, tmp.y);
        playerDirectionalLight.transform.position = transform.position + player2mouse.normalized;

        // Rotate
        Quaternion q = Quaternion.FromToRotation(playerDirectionalLight.transform.up, player2mouse.normalized);
        playerDirectionalLight.transform.rotation = q * playerDirectionalLight.transform.rotation;

        Quaternion rotateFireball = Quaternion.FromToRotation(fireBallPrefab.transform.right, player2mouse.normalized);
        if (isFiring && !isCoolingDown) {
            GameObject bullet = Instantiate(fireBallPrefab, transform.position + player2mouse.normalized, rotateFireball * fireBallPrefab.transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(player2mouse.normalized * bulletForce, ForceMode2D.Impulse);
            StartCoroutine(CoolDownCountdown());

            DoPlayAudioClip();
        }
    }

    void DoPlayAudioClip()
    {
        audio.Play();
    }

    private IEnumerator CoolDownCountdown() {
        isCoolingDown = true;
        yield return new WaitForSeconds(coolDownInterval);
        isCoolingDown = false;
    }
}
