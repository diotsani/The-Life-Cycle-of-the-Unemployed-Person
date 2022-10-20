using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal static class Constants
{
    public static class Name
    {
        public const string Laptop ="Laptop";
        public const string Handphone = "Handphone";
        public const string BookShelf = "Book Shelf";
        public const string Door = "Door";
        public const string Refrigerator = "Refrigerator";
        public const string Wallet = "Wallet";
        
    }
    public static class Scene
    {
        public const string Home = "TestHome";
        public const string Gameplay = "TestGameplay";
    }
    public static class Status
    {
        public const string Skill = "Skill: ";
        public const string Stress = "Stress: ";
        public const string Health = "Health: ";
        public const string Money = "Money: ";
        public const string Book = "Book: ";
        public const string Food = "Food: ";
        public const string Action = "Action: ";
        public const string Day = "Day ";
    }
    public static class DaySystem
    {
        public const string Morning = "Morning";
        public const string MorningSpotlight = "MorningSpotlight";

        public const string Noon = "Noon";

        public const string Afternoon = "Afternoon";
        public const string AfternoonSpotlight = "AfternoonSpotlight";

        public const string Night = "Night";
        public const string NightSpotlight = "NightSpotlight";
    }
    public static class Requirments
    {
        public const string ApplyJob = "Apply Job";
        public const string TakeCourse = "Take Course";
        public const string PlayGame = "Play Game";
        public const string CheckMail = "Check Mail";
        
        public const string SocialMedia = "Social Media";
        
        public const string ReadBook = "Read Book";
        public const string CheckBookShelf = "Check Book Shelf";
        
        public const string Jogging = "Jogging";
        public const string BuyFoodStock = "Buy Food Stock";
        
        public const string Eat = "Eat";
        public const string ThrowFood = "Throw Food";
        public const string CheckFoodStock = "Check Food Stock";
        
        public const string CheckMoney = "Check Money";
        
        public const string Sell = "Sell";
        public const string Repair = "Repair";
        
    }
    public static class Path
    {
        public const string Laptop ="Decision/Laptop";
        public const string Handphone = "Decision/Handphone";
        public const string BookShelf = "Decision/BookShelf";
        public const string Door = "Decision/Door";
        public const string Refrigerator = "Decision/Refrigerator";
        public const string Wallet = "Decision/Wallet";
        
    }
    public static class EndGame
    {
        public const string WinTitle = "Congratulations!";
        public const string LoseTitle = "Too Bad!";
        
        public const string WinDescription = "You Win";
        public const string LoseDescription = "You Lose";
        
        public const string WinDescription30 = "You are a lucky person";
        public const string WinDescription60 = "Your skill enough to get a job";
        public const string WinDescription90 = "You are a genius";
        
        public const string LoseDescriptionSkill = "Your skill is too low";
        public const string LoseDescriptionStress = "Your Stress is Over";
        public const string LoseDescriptionHealth = "Your are too weak to do anything";
    }
    public static class Monologue
    {
        public const string ApplyJobMonolog = "I have to get a job or I will suffer for the rest of my life. Wish me luck!";
        public const string TakeCourseMonolog = "That’s make my head hurt.";
        public const string CheckMailMonolog_1 = "There’s no email.";
        public const string CheckMailMonolog_2 = "It's hard to get a job. Oh God, what should I do now?";
        public const string PlayGameMonolog = "I feel refreshed, I can release my stress.";
        public const string RepairLaptopMonolog = "I have to spend a lot of money to fix it, come on!";
        
        public const string SocialMediaMonolog_1 = "We are looking for a talented designer who will be responsible for the design of our mobile application. They said. I should try to apply for this job.";
        public const string SocialMediaMonolog_2 = "No job info.";
        public const string RepairHandphoneMonolog = "I have to change my phone, this phone is too old. I want a new model of iPhone, but I'm broke.";
        public const string SellHandphoneMonolog = "This is the only thing I can do to survive, I really need money";
        
        public const string ReadBookMonolog = "I guess reading a book is not a bad thing.";
        public const string SellBookMonolog = "Forgive me. Books are a window to the world, but I need money.";
        
        public const string JoggingMonolog = "I need to stretch my muscles, staying at home all the time is not good for my health.";
        public const string BuyFoodMonolog = "I need food to life.";
        
        public const string EatMonolog = "Don’t forget to eat for your own sake.";
        public const string ThrowFoodMonolog = "It's too bad if you have to throw it away.";

        public const string LockRepairMonolog = "I’m broke. I need money.";
        public static string FoodStockMonolog(int value)
        {
            return $"I have {value} food right now";
        }
        
        public static string BookStockMonolog(int value)
        {
            return $"I have {value} book right now";
        }
        public static string CheckMoneyMonolog(int value)
        {
            return $"I have ${value} left in my wallet";
        }
        
        public static string MoneyOverMonolog(int value)
        {
            int positiveValue = Mathf.Abs(value);
            return $"Not Enough Money, You need {positiveValue} money";
        }
    }
    public static class Feedback
    {
        public const string ApplyJobFeedback = "Just wait for the response. You will be informed the next day. Don’t forget to check your email.";
        public const string TakeCourseFeedback = "You make some progress. Keep it up!";
        public const string PlayGameFeedback = "Release your stress a bit is important thing to do.";
        public const string RepairLaptopFeedback = "That’s a lot of money you have to spend.";
        
        public const string RepairHandphoneFeedback = "You need to consider your expenses. Is that worth it to repair your phone?";
        public const string SellHandphoneFeedback = "No phone is okay, right? At least you have more money now.";
        
        public const string ReadBookFeedback = "Do not rush! You need to slow down your pace sometimes. You are still making progress.";
        public const string SellBookFeedback = "Don’t worry! If you get a job, you will buy the book back.";
        
        public const string JoggingFeedback = "Make sure to keep healthy. Releasing your stress is a good thing.";
        public const string BuyFoodFeedback = "It’s worth buying. I guarantee you.";
        
        public const string EatFeedback = "Don’t forget to eat and stay hydrated!";
        public const string ThrowFoodFeedback = "Oh God, this is a waste of food.";
    }
    public static class History
    {
        public const string ApplyJob = "Apply Job";
        public const string TakeCourse = "Take Course";
        public const string PlayGame = "Play Game";
        public const string CheckMail = "Check Mail";
        public const string RepairLaptop = "Repair Laptop";
        
        public const string SocialMedia = "Social Media";
        public const string RepairHandphone = "Repair Handphone";
        public const string SellHandphone = "Sell Handphone";
        
        public const string ReadBook = "Read Book";
        public const string CheckBookShelf = "Check Book Shelf";
        public const string SellBook = "Sell Book";
        
        public const string Jogging = "Jogging";
        public const string BuyFoodStock = "Buy Food Stock";
        
        public const string Eat = "Eat";
        public const string ThrowFood = "Throw Food";
        public const string CheckFoodStock = "Check Food Stock";
        
        public const string CheckMoney = "Check Money";
    }
}
