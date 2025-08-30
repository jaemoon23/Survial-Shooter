using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float Vertical { get; private set; }
    public float Horizontal { get; private set; }
    public bool Fire { get; private set; }

    private void Update()
    {
        Vertical = Input.GetAxis(Defines.AxisVertical);
        Horizontal = Input.GetAxis(Defines.AxisHorizontal);
        Fire = Input.GetButton(Defines.AxisFire1);
    }
}
