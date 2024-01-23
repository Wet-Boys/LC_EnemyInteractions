using LethalCompanyInputUtils.Api;
using UnityEngine.InputSystem;

namespace GemumoddoLcDevTools;

internal class InputActions : LcInputActions
{
    private static InputActions? _instance;

    internal static void SetInstance(InputActions instance) => _instance = instance;
    
    [InputAction("<Keyboard>/z")]
    public InputAction NoClipAction { get; set; }
    
    public static InputAction NoClip => _instance!.NoClipAction;
}