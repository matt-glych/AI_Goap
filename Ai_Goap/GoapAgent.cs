using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// inventory for agents
public class Inventory
{
    Dictionary<string, int> _items;
    public Inventory()
    {
        _items = new Dictionary<string, int>();
    }

    // access items
    public Dictionary<string, int> Items => _items;
}

// base abstract agent class
public abstract class GoapAgent
{
    // agent world state
    protected GoapState worldState;
    // agent goal
    protected GoapGoal goal;
    // available actions
    protected HashSet<GoapAction> availableActions;
    // queue of current actions
    protected Queue<GoapAction> actions;
    // planner
    protected Planner planner;
    // inventory
    protected Inventory inventory;

    public GoapAgent()
    {
        planner = new Planner();
        worldState = new GoapState();
        goal = new GoapGoal();
        availableActions = new HashSet<GoapAction>();   
        actions = new Queue<GoapAction>();
        inventory = new Inventory();
    }

    // update loop
    public abstract void Update();

    // add to available actions
    public void AddAction(GoapAction action)
    {
        availableActions.Add(action);
    }

    // accessors
    public GoapGoal Goal => goal;
    public GoapState WorldState => worldState;
    public HashSet<GoapAction> AvailableActions => availableActions;
    public Queue<GoapAction> Actions => actions;
    public Inventory Inventory => inventory;

       
}
