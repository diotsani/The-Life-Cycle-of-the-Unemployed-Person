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

    public static class Resources
    {
        public const string Decision = "Prefabs/DecisionButton";
        public const string DamagedParticle = "Prefabs/DamagedParticle";
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
        public const string JobInfo = "Job Info";
        
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
        
        public const string WinDescription = "Congratulations, you just proven yourself to your parent that nothing is impossible if you really put your effort on something. You just found a job that you been looking for all this time. You have made your parents proud. But, no any efforts comes without a price, that is your mental health.";
        public const string LoseDescription = "You Lose";
        
        public const string WinDescription30 = "You are a lucky person";
        public const string WinDescription60 = "Your skill enough to get a job";
        public const string WinDescription90 = "You are a genius";
        
        public const string LoseDescriptionSkill = "Your skill is too low";
        public const string LoseDescriptionStress = "Because of your parent pressure, you study so hard that you forget about your mental health. You keep forcing yourself to find a job in a brief time.";
        public const string LoseDescriptionHealth = "Unfortunately, Because you are too focused on developing your skill you forget to check your health. Because of that, you're getting rushed to the hospital.";

        public static string WinMessage(int value)
        {
            return $"If it were scale 0 to 100 your score on mental health is {value}";
        }
        public static string StressMessage(int value)
        {
            return $"If your stress is over, and your qualification is on {value}.";
        }
        public static string HealthMessage(int value)
        {
            return $"If your qualification is scaled from 1 to 100. Approximately, your qualification is on {value}.";
        }
    }
    public static class Monologue
    {
        //Laptop
        public const string ApplyJobMonolog_1 = "I have to get a job or I will suffer for the rest of my life. Wish me luck!.";
        public const string ApplyJobMonolog_2 = "I’m gonna hope for the best.";
        public const string ApplyJobMonolog_3 = "I put all my effort just to apply for this job. Hoping all those times become worth it.";
        
        public const string TakeCourseMonolog_1 = "That makes my head hurt. At least this is the only thing I can do to improve my skill significantly.";
        public const string TakeCourseMonolog_2 = "Ugh, it's hard. At least I am making progress.";
        public const string TakeCourseMonolog_3 = "So, this is how you do this job, ughh…. I think I need to relax a bit.";
        
        public const string CheckMailMonolog_1 = "There’s no email.";
        public const string CheckMailMonolog_2 = "It's hard to get a job. Oh God, what should I do now?";
        public const string CheckMailMonolog_3 = "I think they just forgot my existence.";
        
        public const string PlayGameMonolog_1 = "I can relax for a while, but if I play this game for too long, I can't live anymore. I mean, no work, no money, no food.";
        public const string PlayGameMonolog_2 = "Haha, this is my win. You imbecile!";
        public const string PlayGameMonolog_3 = "This is too easy, I can win anytime that I want.";
        
        public const string RepairLaptopMonolog_1 = "I have to spend a lot of money to fix it, come on!";
        public const string RepairLaptopMonolog_2 = "I’m broke. I need money.";
        public const string RepairLaptopMonolog_3 = "This device is the most essential device in my room, it may cost me a lot of money, but that is money that I’m willing to spend.";
        
        //Hp
        public const string SocialMediaMonolog_1 = "Hooray, my favorite youtuber is streaming.";
        public const string SocialMediaMonolog_2 = "Why is this thing have to appear in my timeline? So irritated.";
        public const string SocialMediaMonolog_3 = "Nothing major is happening, I guess.";
        
        public const string JobInfoMonolog_1 = "We are looking for a talented designer who will be responsible for the design of our mobile application. They said. I should try to apply for this job.";
        public const string JobInfoMonolog_2 = "No job info.";
        public const string JobInfoMonolog_3 = "";
        
        public const string RepairHandphoneMonolog_1 = "I have to change my phone, this phone is too old. I want a new model of iPhone, but I'm broke.";
        public const string RepairHandphoneMonolog_2 = "It may not be the best phone that exists, but it is the one I needed.";
        public const string RepairHandphoneMonolog_3 = "Why does it have to break now of all times.";
        
        public const string SellHandphoneMonolog_1 = "This is the only thing I can do to survive, I really need money";
        public const string SellHandphoneMonolog_2 = "I may need to inform my friends, that I can’t contact them for a while.";
        public const string SellHandphoneMonolog_3 = "Goodbye, my phone. You have served me well.";
        
        //Book
        public const string ReadBookMonolog_1 = "I guess reading a book is not a bad thing.";
        public const string ReadBookMonolog_2 = "They say that book is the window of the world, I may not learn much but it’s better than not learning at all.";
        public const string ReadBookMonolog_3 = "I feel like I have broaden my horizons and slightly change my perspective.";
        
        public const string SellBookMonolog_1 = "Forgive me. I have to sell this book, ‘cause I need money.";
        public const string SellBookMonolog_2 = "Some books get sacrificed for a greater cause.";
        public const string SellBookMonolog_3 = "This book was an incredible read. I hope that anyone who bought it can appreciate it as well.";
        
        //Pintu
        public const string JoggingMonolog_1 = "I need to stretch my muscles, staying at home all the time is not good for my health.";
        public const string JoggingMonolog_2 = "Jogging sure gives me some peace of mind.";
        public const string JoggingMonolog_3 = "Nothing beats a good run.";
        
        public const string BuyFoodMonolog_1 = "I need food to life.";
        public const string BuyFoodMonolog_2 = "Food is the essential thing to have right now.";
        public const string BuyFoodMonolog_3 = "A surplus for my stomach, but deficit on my money.";
        
        //Kulkas
        public const string EatMonolog_1 = "*munch munch much* *glug glug glug* My throat is in heaven right now.";
        public const string EatMonolog_2 = "*munch munch much* *glug glug glug I better treasure the time I’m eating food because I might not eat for a while.";
        public const string EatMonolog_3 = "Hmmm…. the best feeling in the world is a full stomach.";
        
        public const string ThrowFoodMonolog_1 = "It's too bad if you have to throw it away.";
        public const string ThrowFoodMonolog_2 = "May god forgive me, for I have to throw away my food.";
        public const string ThrowFoodMonolog_3 = "A wasted food means wasted money. *sigh*";

        public const string LockRepairMonolog_1 = "I’m broke. I need money.";
        public const string LockRepairMonolog_2 = "";
        
        public static string FoodStockMonolog(int rnd,int value)
        {
            switch (rnd)
            {
                case 0 : return $"I have {value} food right now";
                break;
                case 1 : return $"Some {value} food left in the fridge.”";
                break;
                case 2 : return $"These food looks delicious. But, I must conserve my food. Because I have {value} food left in the fridge";
                break;
            }
            return null;
        }
        
        public static string BookStockMonolog(int rnd,int value)
        {
            switch (rnd)
            {
                case 0 : return $"I have {value} book right now";
                    break;
                case 1 : return $"I have {value} book to read or sell.”";
                    break;
                case 2 : return $"This {value} book is all I have now.";
                    break;
            }
            return null;
        }
        public static string CheckMoneyMonolog(int rnd,int value)
        {
            switch (rnd)
            {
                case 0 : return $"I have ${value} left in my wallet";
                    break;
                case 1 : return $"“I only have ${value} in my wallet.”";
                    break;
                case 2 : return $"Got to save money because I only have ${value} left.";
                    break;
            }
            return null;
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
        public const string RepairLaptopFeedback = "You need to consider your expenses.";
        
        public const string SocialMediaFeedback = "Your friends share something.";
        public const string RepairHandphoneFeedback = "You need to consider your expenses.";
        public const string SellHandphoneFeedback = "No phone is okay, right? At least you have more money now.";

        public const string ReadBookFeedback = "Do not rush! You need to slow down your pace sometimes. You are still making progress.";
        public const string SellBookFeedback = "Don’t worry! If you get a job, you will buy the book back.";
        
        public const string JoggingFeedback = "Make sure to keep healthy. Releasing your stress is a good thing.";
        public const string BuyFoodFeedback = "It’s worth buying.";
        
        public const string EatFeedback = "Don’t forget to eat and stay hydrated!";
        public const string ThrowFoodFeedback = "Your food has expired.";
    }
    public static class History
    {
        public const string ApplyJob = "Apply Job";
        public const string TakeCourse = "Take Course";
        public const string PlayGame = "Play Game";
        public const string CheckMail = "Check Mail";
        public const string RepairLaptop = "Repair Laptop";
        
        public const string SocialMedia = "Social Media";
        public const string JobInfo = "JobInfo";
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
