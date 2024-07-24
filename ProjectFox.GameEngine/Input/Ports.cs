using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.GameEngine.Input;

public static class Ports
{
    public static readonly ICollection<InputDevice> devices = new Array<InputDevice>(0x4);

    internal static void ProcessDevices()
    {
        Array<InputDevice> devs = (Array<InputDevice>)devices;
        for (int i = 0; i < devs.length; i++)
        {
            InputDevice device = devs.elements[i];

            if (device.digitalButtons != null) foreach (DigitalButton button in device.digitalButtons)
                {
                    bool value = false;

                    Array<DigitalButton> bindings = (Array<DigitalButton>)button.bindings;
                    for (int j = 0; j < bindings.length; j++)
                        value = value || bindings.elements[j].value;

                    button.Value = value;//inline?
                }

            if (device.analogButtons != null) foreach (AnalogButton button in device.analogButtons)
                {
                    byte value = 0;

                    Array<AnalogButton> bindings = (Array<AnalogButton>)button.bindings;
                    for (int j = 0; j < bindings.length; j++)
                        value = Math.Max(value, bindings.elements[j].value);//is this okay?

                    //process these first?
                    Array<DigitalButton> dBindings = (Array<DigitalButton>)button.digitalBindings;
                    for (int j = 0; j < dBindings.length; j++)
                        if (dBindings[j].value) value = byte.MaxValue;

                    button.value = value;
                }

            if (device.directionalPads != null) foreach (DirectionalPad dPad in device.directionalPads)
                {
                    Vector.Direction dir = Vector.Direction.Zero;

                    Array<DirectionalPad> bindings = (Array<DirectionalPad>)dPad.bindings;
                    for (int j = 0; j < bindings.length; j++)
                    {
                        //how to do dpad?
                    }

                    bool xNeg = false, xPos = false, yNeg = false, yPos = false;

                    Array<DigitalButton> xNegBindings = (Array<DigitalButton>)dPad.xNegBindings;
                    for (int j = 0; j < xNegBindings.length; j++)
                        xNeg = xNeg || xNegBindings.elements[j].value;

                    Array<DigitalButton> xPosBindings = (Array<DigitalButton>)dPad.xPosBindings;
                    for (int j = 0; j < xPosBindings.length; j++)
                        xPos = xPos || xPosBindings.elements[j].value;

                    Array<DigitalButton> yNegBindings = (Array<DigitalButton>)dPad.yNegBindings;
                    for (int j = 0; j < yNegBindings.length; j++)
                        yNeg = yNeg || yNegBindings.elements[j].value;

                    Array<DigitalButton> yPosBindings = (Array<DigitalButton>)dPad.yPosBindings;
                    for (int j = 0; j < yPosBindings.length; j++)
                        yPos = yPos || yPosBindings.elements[j].value;

                    dPad.value = dir | Vector.FindDirection(Math.FindSign(xNeg, xPos), Math.FindSign(yNeg, yPos));//could this cause sign to be 0b11?
                }

            if (device.sticks != null) foreach (Stick stick in device.sticks)
                {
                    Vector pos = new(0, 0);

                    Array<Stick> bindings = (Array<Stick>)stick.bindings;
                    for (int j = 0; j < bindings.length; j++)
                    {
                        //how to do analog sticks?
                    }

                    stick.Position = pos;//inline?
                }

            if (device.cursors != null) foreach (Cursor cursor in device.cursors)
                    if (cursor.binding != null)
                    {
                        cursor.nativePos = cursor.binding.nativePos;
                        cursor.scaledPos = cursor.binding.scaledPos;
                        cursor.posChanged = cursor.binding.posChanged;
                    }
        }
    }
}