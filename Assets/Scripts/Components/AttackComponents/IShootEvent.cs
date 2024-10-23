using System;

public interface IShootEvent
{
    public event Action Fired;
}