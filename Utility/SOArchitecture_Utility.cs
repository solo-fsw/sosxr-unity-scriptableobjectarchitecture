namespace ScriptableObjectArchitecture
{
    public static class SOArchitecture_Utility
    {
        public const int ASSET_MENU_ORDER_VARIABLES = 121;
        public const int ASSET_MENU_ORDER_EVENTS = 122;
        public const int ASSET_MENU_ORDER_COLLECTIONS = 123;

        public const string ROOT_MENU = "SOSXR/SO Architecture/";

        public const string VARIABLE_SUBMENU = ROOT_MENU + "Variables/";
        public const string COLLECTION_SUBMENU = ROOT_MENU + "Collections/";
        public const string GAME_EVENT = ROOT_MENU + "Game Events/";

        public const string ADVANCED_GAME_EVENT = GAME_EVENT + "Advanced/";
        public const string ADVANCED_VARIABLE_SUBMENU = VARIABLE_SUBMENU + "Advanced/";
        public const string ADVANCED_VARIABLE_COLLECTION = COLLECTION_SUBMENU + "Advanced/";

        public const string EVENT_LISTENER_SUBMENU = ROOT_MENU + "Event Listeners/";
    }
}