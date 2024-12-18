using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine.Input;

public abstract class InputDevice
{
    public InputDevice(int digitalButtonsLength, int analogButtonsLength, int directionalPadsLength, int sticksLength, int cursorsLength)
    {
        digitalButtons = digitalButtonsLength > 0 ? new DigitalButton[digitalButtonsLength] : null;
        analogButtons = analogButtonsLength > 0 ? new AnalogButton[analogButtonsLength] : null;
        directionalPads = directionalPadsLength > 0 ? new DirectionalPad[directionalPadsLength] : null;
        sticks = sticksLength > 0 ? new Stick[sticksLength] : null;
        cursors = cursorsLength > 0 ? new Cursor[cursorsLength] : null;
    }

    protected internal readonly DigitalButton[] digitalButtons;
    protected internal readonly AnalogButton[] analogButtons;
    protected internal readonly DirectionalPad[] directionalPads;
    protected internal readonly Stick[] sticks;
    protected internal readonly Cursor[] cursors;

    //connected?

    public void UpdateValues(bool[] digitalButtonValues, byte[] analogButtonValues, Vector.Direction[] directionalPadValues, Vector[] analogStickValues, Vector[] cursorValues)
    {
        if (digitalButtons != null && digitalButtonValues != null)
            for (int i = 0, l = Math.Min(digitalButtons.Length, digitalButtonValues.Length); i < l; i++)
            {
                DigitalButton button = digitalButtons[i];
                bool value = digitalButtonValues[i];

                button.changed = value != button.value;
                button.value = value;
            }

        if (analogButtons != null && analogButtonValues != null)
            for (int i = 0, l = Math.Min(analogButtons.Length, analogButtonValues.Length); i < l; i++)
                analogButtons[i].value = analogButtonValues[i];

        if (directionalPads != null && directionalPadValues != null)
            for (int i = 0, l = Math.Min(directionalPads.Length, directionalPadValues.Length); i < l; i++)
                directionalPads[i].value = directionalPadValues[i];

        if (sticks != null && analogStickValues != null)
            for (int i = 0, l = Math.Min(sticks.Length, analogStickValues.Length); i < l; i++)
            {
                Stick stick = sticks[i];

                stick.position = analogStickValues[i].Clamp(Stick.MinValue, Stick.MaxValue);//inline?
                stick.xMoved.Value = stick.position.x > stick.deadZone.x || stick.position.x < stick.negDeadZone.x;//inline?
                stick.yMoved.Value = stick.position.y > stick.deadZone.y || stick.position.y < stick.negDeadZone.y;//inline?
            }

        if (cursors != null && cursorValues != null)
            for (int i = 0, l = Math.Min(cursors.Length, cursorValues.Length); i < l; i++)
            {
                Cursor cursor = cursors[i];
                Vector value = cursorValues[i];

                if (cursor.nativePos.Equals(value)) cursor.posChanged = false;
                else
                {
                    cursor.nativePos = value;//clamp to window size?

                    float scale = Screen.Scale;//is the member private?
                    int scaleInt = (int)scale;
                    Vector scaled = Screen.OneToOne ?//is the member private?
                        new(value.x / scaleInt, value.y / scaleInt) :
                        new((int)(value.x / scale), (int)(value.y / scale));

                    scaled = scaled.Clamp(default, Screen.size);

                    if (cursor.scaledPos.Equals(scaled))
                    {
                        cursor.posChanged = false;
                        return;
                    }

                    cursor.scaledPos = scaled;
                    cursor.posChanged = true;
                }
            }
    }
}

//Debug input type with all keyboard keys gamepad inputs and mouse, was this going to go in debug?