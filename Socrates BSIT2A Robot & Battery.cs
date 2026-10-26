using System;
using System.Collections.Generic;

public abstract class Robot {
    private IBattery _battery;
    public string Name {get;}
    public int RemainingEnergy {get {return _battery.RemainingEnergy;}}
    
    public Robot (IBattery _battery, string Name){
        this._battery=_battery;
        this.Name = Name;
    }
    
    public void Recharge(int amount){
        _battery.Recharge(amount);
    }
    
    protected void UseEnergy(int amount){
        _battery.Consume(amount);
    }
    
    public abstract void PerformTask();
}    
public class CleaningRobot : Robot{
    public CleaningRobot(IBattery _battery, string Name) : base(_battery,Name){
    }
    public override void PerformTask(){
        UseEnergy(10);
    }
}
public class DeliveryRobot : Robot{
    public DeliveryRobot(IBattery _battery, string Name) : base(_battery,Name){
    }
    public override void PerformTask(){
        UseEnergy(25);
    }
}
public interface IBattery{
    public int RemainingEnergy{get;}
    public void Consume(int amount);
    public void Recharge(int amount);
}
public class LithiumBattery : IBattery{
    private int _remainingEnergy;
    public int RemainingEnergy{get{return _remainingEnergy;}}
    
    public LithiumBattery(int _remainingEnergy){
        this._remainingEnergy = _remainingEnergy;
    }
    
    public void Consume(int amount){
        _remainingEnergy -= amount;
        
        if (_remainingEnergy < 0){
        _remainingEnergy = 0;
        }
    }
    public void Recharge(int amount){
        _remainingEnergy += amount;
        
        if (_remainingEnergy > 100){
        _remainingEnergy = 100;
        }
    }
}

public class SolarBattery : IBattery{
    private int _remainingEnergy;
    public int RemainingEnergy{get{return _remainingEnergy;}}
    
    public SolarBattery(int _remainingEnergy){
        this._remainingEnergy = _remainingEnergy;
    }
    
    public void Consume(int amount){
        _remainingEnergy -= amount;
        
        if (_remainingEnergy < 0){
        _remainingEnergy = 0;
        }
    }
    public void Recharge(int amount){
        _remainingEnergy += amount;
        
        if (_remainingEnergy > 100){
        _remainingEnergy = 100;
        }
    }
}

public class Program{
    static void Main(){
        CleaningRobot cleaner = new CleaningRobot(new LithiumBattery(100), "Cleaner");
        DeliveryRobot delivery = new DeliveryRobot(new SolarBattery(100), "Delivery");

        cleaner.PerformTask();
        delivery.PerformTask();

        cleaner.Recharge(25);
        delivery.Recharge(10);

        Console.WriteLine($"{cleaner.Name}: {cleaner.RemainingEnergy}");
        Console.WriteLine($"{delivery.Name}: {delivery.RemainingEnergy}");

        List<Robot> robots = new List<Robot> { cleaner, delivery };

        foreach (Robot robot in robots)
        {
            robot.PerformTask();
            Console.WriteLine($"{robot.Name}: {robot.RemainingEnergy}");
        }
    }
}