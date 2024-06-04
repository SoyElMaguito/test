using FarmApp;

public class Cowhouse
{
    int totalMilk = 0;
    public int RecolectedTotalMilk { set; get; }
    int currentCow = 0;
    int maxMilk = 10;
    public int LimitCow { get; }

    public int cowFood;


    List<Cow> cows = new();

    public Cowhouse(int limitCow, int restFood)
    {
        this.LimitCow = limitCow;
        this.cowFood = restFood;
    }
    public bool addCow()
    {
        if (cows.Count >= LimitCow)
            return false;

        cows.Add(new());
        return true;

    }
    public int countCows()
    {
        return cows.Count;
    }

    public int orderCow()
    {
        int extractedMilk = 0;
        if (cows[currentCow].Milk < maxMilk)
        {
            extractedMilk = cows[currentCow].Milk;
            cows[currentCow].Milk = 0;
        }
        else
        {
            extractedMilk = cows[currentCow].Milk - maxMilk;
            cows[currentCow].Milk -= maxMilk;
        }
        RecolectedTotalMilk += extractedMilk;
        if(currentCow<cows.Count-1)
            currentCow++;
        else
            currentCow=0;
        return extractedMilk;
    }


    public bool cowFeed()
    {
        if (cows.Count <= 0)
            return false;
        if (cowFood < 1)
            return false;
        for (int i = 0; i < cows.Count; i++)
        {
            if (cowFood > 0)
            {
                cows[i].Hungry += 1;
                this.cowFood -= 1;
            }

        }
        return true;
    }

    public void cowMilk()
    {
        for (int i = 0; i < cows.Count; i++)
        {
            if (cows[i].createMilk())
            {
                this.totalMilk += 2;
            }
        }
    }
    public List<Cow> cowStatus()
    {
        return cows;
    }


}