using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 15f;
    [SerializeField] float yRange = 5f;

    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float controlPitchFactor;
    [SerializeField] float positionYawFactor;
    [SerializeField] float controlRollFactor;

    [SerializeField] GameObject laser;

    float horizontalThrow;
    float verticalThrow;

    void Start() 
    {
        
    }

    void Update()
    {
        ProcessMovement();
        ProcessRotation();
        ProcessFire();
    }

    private void ProcessRotation()
    {
        float positionPitch = transform.localPosition.y * positionPitchFactor;
        float controlPitch = verticalThrow * controlPitchFactor;

        float pitch =  positionPitch + controlPitch;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = verticalThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessMovement()
    {
        horizontalThrow = Input.GetAxis("Horizontal");
        verticalThrow = Input.GetAxis("Vertical");

        float xOffset = horizontalThrow * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);

        float yOffset = verticalThrow * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFire()
    {
        if(Input.GetButton("Fire1"))
        {       
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;     
            emissionModule.enabled = true;
        }
        else
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = false;
        }
    }
}
