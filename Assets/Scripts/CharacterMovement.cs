using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CharacterMovement : MonoSingleton<CharacterMovement>
{
    
    [SerializeField] [Range(0,30)] private float accelerate;
    [SerializeField] [Range(1,25)] private float maxSpeed;
    [SerializeField] [Range(1,20)] private float startSpeed;
    [SerializeField] [Range(0,1)] private float automaticAccelerate;
    [SerializeField] private float leftWall;
    [SerializeField] private float rightWall;
    [SerializeField] private float groundPosY;
    private float _speed;

    private void FixedUpdate()
    {
        _speed += Time.deltaTime*automaticAccelerate;
        _speed = Mathf.Clamp(_speed, startSpeed, maxSpeed);
        var x = ControlInput.Instance.GetHorizontal()*1.8f;
        x *= GetSpeed();
        GetComponent<Rigidbody>().velocity = new Vector3(x, 0, GetSpeed());
        var pos = transform.position;
        pos.x =  Mathf.Clamp(pos.x, leftWall, rightWall);
        pos.y = Mathf.Clamp(pos.y, groundPosY,3000);
        transform.position = pos;
    }
    
    
    public float GetSpeed()
    {
        if (ControlInput.Instance.GetAccelerate())
        {
            return accelerate + _speed;
        }
        return _speed;
    }

    public float GetGroundPosY()
    {
        return this.groundPosY;
    }
    
    public void SetSpeed(float value)
    {
        _speed = value;
    }

    public float GetStartSpeed()
    {
        return startSpeed;
    }
    
}
