namespace ProjectFox.GameEngine.Input;

public class GamepadInputDevice : InputDevice
{
    public GamepadInputDevice() : base(10, 2, 1, 2, 0)
    {
        //check that button numbers match
        cross = a = b0 = digitalButtons[0] = new();
        circle = b = b1 = digitalButtons[1] = new();
        square = x = b2 = digitalButtons[2] = new();
        triangle = y = b3 = digitalButtons[3] = new();
        start = b4 = digitalButtons[4] = new();
        select = back = b5 = digitalButtons[5] = new();
        l1 = leftShoulder = b6 = digitalButtons[6] = new();
        r1 = rightShoulder = b7 = digitalButtons[7] = new();
        l3 = leftStickButton = b8 = digitalButtons[8] = new();
        r3 = rightStickButton = b9 = digitalButtons[9] = new();

        dPad = directionalPads[0] = new();

        l2 = leftTrigger = analogButtons[0] = new();
        r2 = rightTrigger = analogButtons[1] = new();

        leftStick = sticks[0] = new();
        rightStick = sticks[1] = new();
    }

    public readonly DigitalButton b0, b1, b2, b3, b4, b5, b6, b7, b8, b9,
        a, b, x, y, start, back, leftShoulder, rightShoulder, leftStickButton, rightStickButton,
        cross, circle, square, triangle, select, l1, r1, l3, r3;
    public readonly DirectionalPad dPad;
    public readonly AnalogButton leftTrigger, rightTrigger, l2, r2;
    public readonly Stick leftStick, rightStick;
}