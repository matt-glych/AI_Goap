using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GoapState
{
    protected HashSet<KeyValuePair<string, Object>> _state;

    public GoapState()
    {
        _state = new HashSet<KeyValuePair<string, object>>();
    }
    public GoapState(string key, Object value)
    {
        _state = new HashSet<KeyValuePair<string, object>>();
        _state.Add(new KeyValuePair<string, object>(key, value));
    }

    // return the state
    public HashSet<KeyValuePair<string, Object>> State => _state;

    // add a state
    public void Add(string key, object value)
    {
        _state.Add(new KeyValuePair<string, object>(key, value));
    }

    // return a new copy of the state
    public GoapState Copy()
    {
        GoapState state = new GoapState();
        HashSet<KeyValuePair<string, object>> copy = new HashSet<KeyValuePair<string, Object>>();

        foreach (KeyValuePair<string, object> pair in _state)
            copy.Add(pair);

        state._state = copy;
        return state;
    }

    // apply effects of another world state to this that, adding them if not existing (not generating a new state)
    public void ApplyEffectsHard(GoapState alterState)
    {
        foreach (KeyValuePair<string, object> alt in alterState.State)
        {
            bool exists = false;

            foreach (KeyValuePair<string, object> s in _state)
            {
                if (s.Key.Equals(alt.Key))
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                this.State.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(alt.Key); });
               
            }

            KeyValuePair<string, object> stateChange = new KeyValuePair<string, object>(alt.Key, alt.Value);
            this.State.Add(stateChange);
        }

    }

    // apply effects of this state to a another state adding them if non-esisting (generating a new state)
    public GoapState ApplyEffects(GoapState alterState)
    {
        GoapState copy = this.Copy();

        foreach (KeyValuePair<string, object> alt in alterState.State)
        {
            bool exists = false;

            foreach (KeyValuePair<string, object> s in _state)
            {
                if (s.Equals(alt))
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                this.State.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(alt.Key); });
               
            }

            KeyValuePair<string, object> stateChange = new KeyValuePair<string, object>(alt.Key, alt.Value);
            copy.State.Add(stateChange);
        }

        return copy;
    }

    // check this state exists in another state
    public bool ExistsInState(GoapState test)
    {
        bool result = true;

        foreach (KeyValuePair<string, object> s in _state)
        {
            bool match = false;

            foreach (KeyValuePair<string, object> t in test.State)
            {
                //Console.WriteLine("t: {0}:{1}   s: {2}:{3}", t.Key, t.Value, s.Key, s.Value);
                if (s.Equals(t))
                {
                    match = true;
                    break;
                }
            }
            if (!match)
                result = false;
        }

        return result;
    }
}

