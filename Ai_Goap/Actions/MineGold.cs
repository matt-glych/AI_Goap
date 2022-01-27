using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_GOAP
{
    public class MineGold : GoapAction
    {

        public MineGold(string name, float cost, float duration):base(name, cost, duration)
        {
        }

        // perform mining action
        public override void Perform(GoapAgent agent)
        {
            base.Perform(agent);


            // reduce the health of the tool
            agent.Inventory.Items["tool"]--;

            Console.WriteLine("The day is spent mining and gold is found.");
            // tool health behaviour
            switch (agent.Inventory.Items["tool"])
            {

                case 5:
                    Console.WriteLine("The tool is in good working order.");
                    break;

                case 4:
                    break;

                case 3:
                    Console.WriteLine("The tool is staring to wear.");
                    break;

                case 2:
                    break;

                case 1:
                    Console.WriteLine("The tool is about to break.");
                    break;

                case 0:
                    // remove tool from inventory when health reaches zero
                    Console.WriteLine("The tool has broken. Need to collect a new one.");
                    agent.Inventory.Items.Remove("tool");
                    break;
            }
        }
    }
}
