﻿using ProjectFox.CoreEngine.Math;
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
                sticks[i].position = analogStickValues[i].Clamp(-127, 127);

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

#if DEBUG
public class NESInputDevice : InputDevice//move to different library later
{
    public NESInputDevice() : base(4, 0, 1, 0, 0)
    {
        a = digitalButtons[0] = new();
        b = digitalButtons[1] = new();
        start = digitalButtons[2] = new();
        select = digitalButtons[3] = new();

        dPad = directionalPads[0] = new();
    }

    public readonly DigitalButton a, b, start, select;
    public readonly DirectionalPad dPad;
}
#endif