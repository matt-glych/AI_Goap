using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_GOAP
{
    public class PickupGold : GoapAction
    {
        public PickupGold(string name, float cost, float duration) : base(name, cost, duration)
        {
        }

        // perform pickup gold action
        public override void Perform(GoapAgent agent)
        {
            base.Perform(agent);

            // add gold to inventory or incement the amount of curent gold
            if (!agent.Inventory.Items.ContainsKey("gold"))
                agent.Inventory.Items.Add("gold", 1);
            else
            {
                agent.Inventory.Items["gold"]++;
            }

            Console.WriteLine("The gold is dug up.");
        }
    }
}
