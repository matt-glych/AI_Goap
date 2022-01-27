using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Planner
{
    public Queue<GoapAction> Plan(GoapGoal goal, GoapState state, HashSet<GoapAction> actions, int maxDepth = 100)
    {

        Queue<GoapAction> result = new Queue<GoapAction>();

        if (maxDepth <= 0)
            return result;

        if(goal.ExistsInState(state))
            return result;

        //------------------------------------------------------
        // TODO --- add check for procedural/agent preconditions
        //------------------------------------------------------

        // build tree
        List<Node> leaves = new List<Node>();

        // build graph
        Node start = new Node(null, 0, null, state);
        bool success = Graph(start, leaves, actions, goal);

        if (!success)
        {
            Console.WriteLine("No plan!");
            return null;
        }

        // find the cheapest
        Node cheapest = null;

        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.cost < cheapest.cost)
                    cheapest = leaf;
            }
        }

        List<GoapAction> actionList = new List<GoapAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null)
            {
                actionList.Insert(0, n.action);
                //Console.WriteLine("A :" + n.action._name);
            }
            n = n.parent;
        }

        // order the actions
        foreach (var item in actionList)
        {
            result.Enqueue(item);
        }

        // return the plan
        return result;
    }

    // graph the actions to create plans
    private bool Graph(Node parent, List<Node> leaves, HashSet<GoapAction> actions, GoapGoal goal)
    {
        bool solutionFound = false;
        
        foreach (GoapAction action in actions)
        {
            // check if action's preconditions exist in the current state
            if(action.Preconditions.ExistsInState(parent.state))
            {
                // if so, create a new state based on the actions effects
                GoapState currentState = parent.state.ApplyEffects(action.Effects);
                // create a node based on the new effected state, calculate the costs of the path
                Node currentNode = new Node(parent, parent.cost + action._cost, action, currentState);

                // if the goal exists in the new state, a solution is found for this action
                if (goal.ExistsInState(currentState))
                {
                    leaves.Add(currentNode);
                    solutionFound = true;
                }
                // else remove the action perform the graph again on action subset
                else
                {
                    HashSet<GoapAction> subset = ActionSubset(actions, action);
                    //actions.Remove(action);
                    bool found = Graph(currentNode, leaves, subset, goal);
                    if (found)
                        solutionFound = true;
                }
            }
        }

        // return the result of the graph
        return solutionFound;
    }

    // action list subsetter
    private HashSet<GoapAction> ActionSubset(HashSet<GoapAction> actions, GoapAction toRemove)
    {
        HashSet<GoapAction> result = new HashSet<GoapAction>();
        foreach (var item in actions)
        {
            if(!item.Equals(toRemove))
                result.Add(item);
        }
        return result;
    }

}

