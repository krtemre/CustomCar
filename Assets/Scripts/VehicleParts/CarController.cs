using Assets.Scripts.VehicleParts.Engine;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Engine engine;

    [SerializeField] private KeyCode AccelartingKeyCode;
    [SerializeField] private KeyCode DeaccelertingKeyCode;

    private InputTypesEnum inputType;
    private float SpeedInKMH;

    // Start is called before the first frame update
    void Start()
    {
        if (engine is null)
        {
            Debug.LogError("Engine is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(AccelartingKeyCode))
        {
            inputType = InputTypesEnum.Acceleration;
        }
        else if (Input.GetKeyDown(DeaccelertingKeyCode))
        {
            inputType = InputTypesEnum.Deacceleration;
        }
        else
        {
            inputType = InputTypesEnum.None;
        }

        SpeedInKMH = engine.EngineInput(inputType);
        MoveBody();
    }

    private void MoveBody()
    {
        float speedINMS = SpeedInKMH / 3.6f;

        var pos = this.transform.position;

        pos.z += speedINMS * Time.deltaTime;

        this.transform.position = pos;
    }
}
