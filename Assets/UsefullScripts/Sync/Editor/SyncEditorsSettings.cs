using Gabenwithrock;

namespace Networking.Editor
{
    public class SyncEditorsSettings: SettingsScriptableObject<SyncEditorsSettings>
    {
        public string Postfix = " (Sync)";
        public bool Sync;
        public bool PrintLog;
    }
}