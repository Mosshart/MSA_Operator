using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Events;

namespace MSAEventAggregator.Core
{
    public class Events : PubSubEvent<string>{}
    public class AddToListEvent : PubSubEvent<string>{}
    public class LocalizeEvent : PubSubEvent<bool>{}
    public class MapDetailEvent : PubSubEvent<object>{}
    public class AnimateOverlay : PubSubEvent<bool> {}
    public class AddPin : PubSubEvent<bool> {}
}
