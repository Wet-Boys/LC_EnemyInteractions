using System;
using BepInEx;
using GemumoddoLcDevTools.Utils;

namespace GemumoddoLcDevTools;

[BepInDependency("com.rune580.LethalCompanyInputUtils")]
[BepInPlugin(ModGuid, ModName, ModVersion)]
public class GemumoddoLcDevToolsPlugin : BaseUnityPlugin
{
    public const string ModGuid = "com.gemumoddo.lc_dev_tools";
    public const string ModName = "Gemumoddo Dev Tools";
    public const string ModVersion = "1.0.0";

    private void Awake()
    {
        Logging.SetLogSource(Logger);
        
        InputActions.SetInstance(new InputActions());
        
        ToolModules.InitHooks();
    }
}