using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public abstract class GoapAction
{
    // Action properties
    public string _name;
    private GoapState _preconditions;
    private GoapState _effects;
    public float _cost;
    public float _duration;
    protected bool _complete = false;

    public GoapAction(string name = "", float cost = 0, float duration = 0)
    {
        _preconditions = new GoapState();
        _effects = new GoapState();
        _name = name;
        _cost = cost;
        _duration = duration;
    }

    // accessors
    public GoapState Preconditions => _preconditions;
    public GoapState Effects => _effects;

    // check agent conditions
    public virtual bool CheckAgentConditions(GoapAgent agent)
    {
        return true;
    }

    // perform the action
    public virtual void Perform(GoapAgent agent)
    {
        //Console.WriteLine("Performed action {0}", _name);
        Thread.Sleep((int)(_duration * 1000));
        _complete = true;
    }

    // reset action 
    public virtual void Reset()
    {
        _complete = false;
    }

    // return completion status
    public bool Complete => _complete;
}
