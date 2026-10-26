using System;
using System.Collections.Generic;

public class Program {
  public static void Main(String[] args){
    Car car = new Car("101", "Toyota", 100, 5);
    Motorcycle motorcycle = new Motorcycle("102", "Yamaha", 50, 150);
    List<Vehicle> vehicles = new List<Vehicle> { car, motorcycle };
    Console.WriteLine("=== Vehicle Rental Costs ===");
    foreach (Vehicle vehicle in vehicles) {
        vehicle.displayInfo();
        Console.WriteLine($"Cost for 3 days: ${vehicle.calculateRentalCost(3):F2}");
    }
    Customer customer = new Customer("Alice", "AB1234");
    customer.rentVehicle(car);
    Console.WriteLine("\n=== Customer Rental Summary ===");
    customer.printRentalSummary(5);
  }
}

public class Customer{
    private string name {get;set;}
    private string licenseNumber {get;set;}
    private Vehicle rentedVehicle;
    
    public Customer(string name, string licenseNumber){
        this.name = name;
        this.licenseNumber = licenseNumber;
    } 
    
    
    public void rentVehicle(Vehicle v){
        rentedVehicle = v;
        
    }
    
    public void returnVehicle(){
        rentedVehicle = null;
    }
    
    public void printRentalSummary(int days){
        if (rentedVehicle == null) {
            Console.WriteLine("No vehicle is currently rented.");
            return;
        }
        Console.WriteLine($"Customer: {name} (License: {licenseNumber})");
        rentedVehicle.displayInfo();
        Console.WriteLine($"Total Cost for {days} days: ${rentedVehicle.calculateRentalCost(days):F2}");
    }
}

public class Vehicle{
    protected string vehicleId {get;set;}
    protected string brand {get;set;}
    protected double dailyRate {get;set;}
    
    public Vehicle(string vehicleId, string brand, double dailyRate){
        this.vehicleId = vehicleId;
        this.brand = brand;
        this.dailyRate = dailyRate;
    }
    
    public virtual double calculateRentalCost(int days){
        return dailyRate * days;
    }
    
    public virtual void displayInfo(){
        Console.WriteLine($"Vehicle [ID={vehicleId}, Brand={brand}]");
    }
}

public class Car : Vehicle{
    
    private int numSeats{get;set;}
    
    public Car(string vehicleId, string brand, double dailyRate, int numSeats) : base(vehicleId, brand, dailyRate){
        this.numSeats = numSeats;
    }
    public override double calculateRentalCost(int days){
        return dailyRate * days * 1.10;
    }
    public override void displayInfo(){
        Console.WriteLine($"Car [ID={vehicleId}, Brand={brand}, Seats={numSeats}]");
    }
}

public class Motorcycle : Vehicle{
    private int engineCapacity {get; set;}
    
    public Motorcycle(string vehicleId, string brand, double dailyRate, int engineCapacity) : base(vehicleId, brand, dailyRate){
       this.engineCapacity = engineCapacity; 
    }
    public override double calculateRentalCost(int days){
        return dailyRate * days * 0.95;
    }
    public override void displayInfo(){
        Console.WriteLine($"Motorcycle [ID={vehicleId}, Brand={brand}, CC={engineCapacity}]");
    }
}