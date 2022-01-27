using System;
using System.Collections.Generic;

/// <summary>
/// Node class used for graphing Goap Actions
/// </summary>
public class Node
{
    public Node parent;
    public float cost;
    public GoapAction action;
    public GoapState state;

    public Node(Node parent, float cost, GoapAction action, GoapState state)
    {
        this.parent = parent;
        this.cost = cost;
        this.action = action;
        this.state = state;
    }
}
