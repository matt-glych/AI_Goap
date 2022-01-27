using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_GOAP
{
    public class CollectTool : GoapAction
    {

        public CollectTool(string name, float cost, float duration):base(name, cost, duration)
        {
        }

        // perform collect tool action
        public override void Perform(GoapAgent agent)
        {
            base.Perform(agent);

            // add tool to inventory with a health value
            if (!agent.Inventory.Items.ContainsKey("tool"))
                agent.Inventory.Items.Add("tool", 5);

            Console.WriteLine("Collected a tool to use for mining.");
        }
    }
}
