public class Farmer
{
    public int Salary {set; get;}
    public int workinghours;
    public int CostHours{get{ return 2;}}
    public void addHours(int workHours)
    {
        this.workinghours += workHours; 
    }
}