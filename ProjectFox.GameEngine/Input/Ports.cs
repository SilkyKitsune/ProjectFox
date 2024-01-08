using static ProjectFox.CoreEngine.Math.Math;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.GameEngine.Input;

public static class Ports//is this class redundant?
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
                Array<DigitalButton> bindings = (Array<DigitalButton>)button.bindings;
                bool value = false;
                for (int j = 0; j < bindings.length; j++)
                    value = value || bindings.elements[j].value;
                button.Value = value;//inline?
            }

            foreach (AnalogButton button in device.analogButtons)
            {
                Array<AnalogButton> bindings = (Array<AnalogButton>)button.bindings;
                byte value = 0;
                for (int j = 0; j < bindings.length; j++)
                    value = Max(value, bindings.elements[j].value);//is this okay?

                Array<DigitalButton> dBindings = (Array<DigitalButton>)button.digitalBindings;
                for (int j = 0; j < dBindings.length; j++)
                    if (dBindings[j].value) value = byte.MaxValue;//is this okay?

                button.value = value;
            }

            //how to do dpad?

            foreach (Stick stick in device.sticks)
            {
                Array<Stick> bindings = (Array<Stick>)stick.bindings;
                //how to do analog sticks?
            }

            foreach (Cursor cursor in device.cursors)
                if (cursor.binding != null)
                    cursor.Position = cursor.binding.Position;//inline?
        }
    }
}
