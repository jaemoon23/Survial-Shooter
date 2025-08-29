using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float Move { get; private set; }
    public float Roate { get; private set; }
    public bool Fire { get; private set; }

    private void Update()
    {
        Move = Input.GetAxis(Defines.AxisVertical);
        Roate = Input.GetAxis(Defines.AxisHorizontal);
        Fire = Input.GetButton(Defines.AxisFire1);
        
    }
}
