using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot 
{
    public Vector3 CurrentPosition {  get; set; }
    public Vector3 LastPosition { get; set; }
    public bool IsLocked { get; set; }
    public List<Connection> Connections { get; } = new List<Connection>();

    public Dot(Vector3 initialPosition, bool isLocked)
    {
        CurrentPosition = initialPosition;
        LastPosition = initialPosition;
        IsLocked = isLocked;
    }
    
    public static Connection Connect(Dot dotA, Dot dotB, float length = -1f)
    {
        Connection connection = length < 0f
            ? new Connection(dotA, dotB) 
            : new Connection(dotA, dotB, length);

        dotA.Connections.Add(connection);
        dotB.Connections.Add(connection);
        return connection;
    }
    
    public static void Disconnect(Connection connection)
    {
        List<Connection> dotAconnections = connection.DotA.Connections;
        List<Connection> dotBconnections = connection.DotB.Connections;

        if(dotAconnections.Contains(connection))
            dotAconnections.Remove(connection);

        if (dotBconnections.Contains(connection))
            dotBconnections.Remove(connection);
    }
}
