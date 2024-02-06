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

            foreach (DigitalButton button in device.digitalButtons)
            {
                bool value = false;

                Array<DigitalButton> bindings = (Array<DigitalButton>)button.bindings;
                for (int j = 0; j < bindings.length; j++)
                    value = value || bindings.elements[j].value;

                button.Value = value;//inline?
            }

            foreach (AnalogButton button in device.analogButtons)
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

            foreach (DirectionalPad dPad in device.directionalPads)
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
                    xNeg = xNeg || xNegBindings.elements[i].value;

                Array<DigitalButton> xPosBindings = (Array<DigitalButton>)dPad.xNegBindings;
                for (int j = 0; j < xPosBindings.length; j++)
                    xPos = xPos || xPosBindings.elements[i].value;

                Array<DigitalButton> yNegBindings = (Array<DigitalButton>)dPad.xNegBindings;
                for (int j = 0; j < yNegBindings.length; j++)
                    yNeg = yNeg || yNegBindings.elements[i].value;

                Array<DigitalButton> yPosBindings = (Array<DigitalButton>)dPad.xNegBindings;
                for (int j = 0; j < yPosBindings.length; j++)
                    yPos = yPos || yPosBindings.elements[i].value;

                Math.Sign x = Math.FindSign(xNeg, xPos), y = Math.FindSign(yNeg, yPos);
                //combine with dir

                dPad.value = dir;
            }

            foreach (Stick stick in device.sticks)
            {
                Vector pos = new(0, 0);

                Array<Stick> bindings = (Array<Stick>)stick.bindings;
                for (int j = 0; j < bindings.length; j++)
                {
                    //how to do analog sticks?
                }

                stick.Position = pos;//inline?
            }

            foreach (Cursor cursor in device.cursors)
                if (cursor.binding != null)
                    cursor.Position = cursor.binding.Position;//inline?
        }
    }
}
