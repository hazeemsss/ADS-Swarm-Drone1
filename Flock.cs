

public class Flock
{
    Drone[] agents;
    int num;
    
    public Flock(int maxnum)
    {
        agents = new Drone[maxnum];
    }
    
    // actually add the drones
    public void Init(int num)
    {
        this.num = num;
        for (int i=0; i<num; i++)
        {
            agents[i] = new Drone(i);
        }
    }
    
    public void Update()
    {
        for (int i=0; i<num; i++)
        {
            agents[i].Update();
        }
    }
    
    public float average() 
    {
        float sum = 0;
        for (int i = 0; i < num; i++)
        {
            sum += agents[i].Battery;
        }
        return sum / num;
    }

    public int max()
    {
        float maxValue = agents[0].Battery;
        for (int i = 1; i < num; i++)
        {
            if (agents[i].Battery > maxValue)
            {
                maxValue = agents[i].Battery;
            }
        }
        return (int)maxValue;  // assuming battery is the field of interest
    }

    public int min()
    {
        float minValue = agents[0].Battery;
        for (int i = 1; i < num; i++)
        {
            if (agents[i].Battery < minValue)
            {
                minValue = agents[i].Battery;
            }
        }
        return (int)minValue;
    }   

    public void print()
    {
    }

    public void append(Drone val)
    {
    }

    public void appendFront(Drone val)
    {
    }


    public void insert(Drone val, int index)
    {

    }

    public void deleteFront(int index)
    {

    }

    public void deleteBack(int index)
    {

    }


    public void delete(int index)
    {

    } 
    
    
    public void bubblesort()
    {
        for (int i = 0; i < num - 1; i++)
        {
            for (int j = 0; j < num - i - 1; j++)
            {
                if (agents[j].Battery > agents[j + 1].Battery)
                {
                    // Swap drones
                    Drone temp = agents[j];
                    agents[j] = agents[j + 1];
                    agents[j + 1] = temp;
                }
            }
        }
    }

    public void insertionsort()
    {
        for (int i = 1; i < num; i++)
        {
            Drone key = agents[i];
            int j = i - 1;
            while (j >= 0 && agents[j].Battery > key.Battery)
            {
                agents[j + 1] = agents[j];
                j = j - 1;
            }
            agents[j + 1] = key;
        }
    }
}