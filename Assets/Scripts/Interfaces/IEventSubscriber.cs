public interface IEventSubscriber
{
    /// <summary>
    /// Declare explicitly: void IEventSubscriber.SubscribeToEvents() { } <br/>
    /// Use explicit calling: ((IEventSubscriber)this).SubscribeToEvents(); <br/>
    /// This is done to hide this method.
    /// </summary>
    void SubscribeToEvents();

    /// <summary>
    /// ALWAYS UNSUBSCRIBE. <br/>
    /// Declare explicitly: void IEventSubscriber.UnsubscribeFromEvents() <br/>
    /// Use explicit calling: ((IEventSubscriber)this).UnsubscribeFromEvents(); <br/>
    /// This is done to hide this method.
    /// </summary>
    void UnsubscribeFromEvents();
}
