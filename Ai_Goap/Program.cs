
namespace AI_GOAP
{
    public class Program
    {
        static void Main(string[] args)
        {
            // agent
            Miner agent = new Miner();

            while (true)
            { 
                // run the agent
                agent.Update();
            }
        }
    }
}