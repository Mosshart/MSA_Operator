using Prism.Events;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MSAEventAggregator.Core
{
    public class Events : PubSubEvent<string>{}
    public class AddToListEvent : PubSubEvent<string>{}
    public class LocalizeEvent : PubSubEvent<bool>{}
    public class MapDetailEvent : PubSubEvent<object>{}
    public class AnimateOverlay : PubSubEvent<bool> {}
    public class AddPin : PubSubEvent<bool> {}
    public class LogoutEvent : PubSubEvent<bool> {}
    public class CameraWindowEvent : PubSubEvent<bool> {}
    public class LocalizationFindEvent : PubSubEvent<bool> {}
    public class CenterLocationChange : PubSubEvent<string> { };
    public class CloseLocalizationDetails : PubSubEvent { };
}
