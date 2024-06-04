public class Cow
{
    public int Milk { get; set; }
    public int Hungry { get; set; }

    public bool createMilk()
    {
        if (Hungry < 2)
            return false;

        Hungry -= 2;
        Milk++;
        return true;
    }
}