using System;

public interface IHasProgress 
{
    public event EventHandler<onProgressChangedEventArgs> onSpeedChanged;

    public class onProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
