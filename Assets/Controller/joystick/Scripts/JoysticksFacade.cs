using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoysticksFacade : MonoBehaviour
{
    static JoystickInfo[] _joysticks;
    [SerializeField] JoystickInfo[] Joysticks;

    void Start()
    {
        if (_joysticks != null)
            Debug.LogError("There has two joysticksFacade object in the scene. Check it out!");
        _joysticks = Joysticks;
        for (int i = 0; i < _joysticks.Length; i++)
        {
            for (int j = 0; j < _joysticks.Length; j++)
            {
                if (i == j) continue;
                else
                {
                    if (_joysticks[i].Name == _joysticks[j].Name)
                    {
                        Debug.LogError("In the joystick facade you have some joystick with the same name");
                    }
                }
            }
        }
    }

    public static JoystickInfo GetJoystick(JoystickName joystick)
    {
        return _joysticks.First(x => x.Name == joystick);
    }
}

public enum JoystickName
{
    left,
    right
}

[System.Serializable]
public class JoystickInfo
{
    public JoystickName Name { get { return name; } }
    [SerializeField] JoystickName name;
    [SerializeField] VirtualJoystick joystick;

    public float GetHorizontalAxis()
    {
        return joystick.JoystickOutput.x;
    }
    public float GetVerticalAxis()
    {
        return joystick.JoystickOutput.y;
    }
}
