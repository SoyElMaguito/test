namespace FarmApp;

public class Farm
{

    int money = 0;
    int milkPrice = 50;
    int cowPrice = 200;
    int priceFood = 10;
    Cowhouse cowhouse = new Cowhouse(6, 20);

    Farmer farmer = new();
    public Farm(int money)
    {
        this.money = money;
    }
    public void cowFeed()
    {
        if (cowhouse.cowFeed())
            farmer.addHours(4);


    }
    public List<Cow> cowStatus()
    {
        return cowhouse.cowStatus();
    }
    public Dictionary<string, object> getStatus()
    {
        Dictionary<string, object> status = new Dictionary<string, object>(){
           {"money", money},
           {"countCows", cowhouse.countCows()},
           {"limitCows", cowhouse.LimitCow},
           {"cowFood", cowhouse.cowFood},
           {"milkPrice",milkPrice},
           {"workingHours", farmer.workinghours},
           {"milkCollected",cowhouse.RecolectedTotalMilk},
           {"foodPrice", priceFood},
           {"workedPrice",farmer.CostHours},
           {"payDay",farmer.Salary}
        };
        return status;
    }

    public int sellMilk()
    {
        money += cowhouse.RecolectedTotalMilk * milkPrice;
        cowhouse.RecolectedTotalMilk = 0;
        return money;
    }

    public bool buyCow()
    {
        if (money < cowPrice)
            return false;
        if (!cowhouse.addCow())
            return false;

        money -= cowPrice;
        return true;

    }

    public void collectMilk()
    {
        if (cowhouse.orderCow() > 0)
            farmer.addHours(8);

    }
    public void simulate()
    {
        cowhouse.cowMilk();
    }
    public void payDay()
    {
        if (money < farmer.CostHours * farmer.workinghours)
            return;
        farmer.Salary = farmer.CostHours * farmer.workinghours;
        money -= farmer.CostHours * farmer.workinghours;
        farmer.workinghours = 0;

    }
    public void buyFood()
    {
        if (money < priceFood)
            return;
        money -= priceFood;
        cowhouse.cowFood++;
    }


}