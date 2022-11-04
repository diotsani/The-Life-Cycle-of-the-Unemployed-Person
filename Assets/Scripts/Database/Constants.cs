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
        public const string Home = "Home";
        public const string Gameplay = "Gameplay";
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
        public const string Player = "PlayerData/PlayerData";
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

        public static string WinDescription(int value)
        {
            return $"Congratulations, you proved to your parent that nothing is impossible if you really put your effort into something. You just found a job you have been looking for all this time. You have made your parents proud. But, no any efforts comes without a price, that is your mental health. If it were a scale of 0 to 100 your score on mental health is {value}. The higher the score, the higher the stress.";
        }
       // Lose Description
        public const string LoseDescription = "Unfortunately, you have exceeded the time limit that your parent has set. Because of that, they ask you to come back to your hometown. They will get you married to someone you hardly know of.";
        public const string LoseDescriptionStress = "Because of your parent pressure, you study so hard that you forget about your mental health. You keep forcing yourself to find a job in a brief time.";
        public const string LoseDescriptionHealth = "Unfortunately, Because you are too focused on developing your skill you forget to check your health. Because of that, you're getting rushed to the hospital.";
        
        public static string LoseMessage(int value)
        {
            return $"In the end, you could not fulfill your dreams, as a woman of career. If your qualification is scaled from 0 to 100, maybe your skill is on {value}";
        }
        public static string StressMessage(int value)
        {
            return $"If your qualification is scaled from 1 to 100. Approximately, your qualification is on {value}.";
        }
        public static string HealthMessage(int value)
        {
            return $"If your qualification is scaled from 1 to 100. Approximately, your qualification is on {value}.";
        }
    }
    public static class Monologue
    {
        //Laptop
        public static string ApplyJobMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return "I have to get a job or I will suffer for the rest of my life. Wish me luck!.";
                case 1: return "I’m gonna hope for the best.";
                case 2 : return "I put all my effort just to apply for this job. Hoping all those times become worth it.";
            }
            return null;
        }
        
        public static string TakeCourseMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"That makes my head hurt. At least this is the only thing I can do to improve my skill significantly.";
                case 1: return"Ugh, it's hard. At least I am making progress.";
                case 2 : return"So, this is how you do this job, ughh…. I think I need to relax a bit.";
            }
            return null;
        }
        
        public const string CheckMailMonolog_1 = "There’s no email.";
        public const string CheckMailMonolog_2 = "It's hard to get a job. Oh God, what should I do now?";
        public const string CheckMailMonolog_3 = "I think they just forgot my existence.";
        public static string CheckMailMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"There’s no email.";
                case 1: return"It's hard to get a job. Oh God, what should I do now?";
                case 2 : return"I think they just forgot my existence.";
            }
            return null;
        }
        
        public static string PlayGameMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return "I can relax for a while, but if I play this game for too long, I can't live anymore. I mean, no work, no money, no food.";
                case 1: return "Haha, this is my win. You imbecile!";
                case 2 : return "This is too easy, I can win anytime that I want.";
            }
            return null;
        }
        
        public static string RepairLaptopMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"I have to spend a lot of money to fix it, come on!";
                case 1: return "I’m broke. I need money.";
                case 2 : return"This device is the most essential device in my room, it may cost me a lot of money, but that is money that I’m willing to spend.";
            }
            return null;
        }
        //Hp
        public const string SocialMediaMonolog_1 = "Hooray, my favorite youtuber is streaming.";
        public const string SocialMediaMonolog_2 = "Why is this thing have to appear in my timeline? So irritated.";
        public const string SocialMediaMonolog_3 = "Nothing major is happening, I guess.";
        public static string SocialMediaMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"Hooray, my favorite youtuber is streaming.";
                case 1: return"Why is this thing have to appear in my timeline? So irritated.";
                case 2 : return"Nothing major is happening, I guess.";
            }
            return null;
        }
        
        public const string JobInfoMonolog_1 = "We are looking for a talented designer who will be responsible for the design of our mobile application. They said. I should try to apply for this job.";
        public const string JobInfoMonolog_2 = "No job info.";
        public const string JobInfoMonolog_3 = "";
        public static string JobInfoMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"We are looking for a talented designer who will be responsible for the design of our mobile application. They said. I should try to apply for this job.";
                case 1: return"No job info.";
            }
            return null;
        }
        
        public static string RepairHandphoneMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"I have to change my phone, this phone is too old. I want a new model of iPhone, but I'm broke.";
                case 1: return"It may not be the best phone that exists, but it is the one I needed.";
                case 2 : return"Why does it have to break now of all times.";
            }
            return null;
        }
        
        public static string SellHandphoneMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"This is the only thing I can do to survive, I really need money";
                case 1: return"I may need to inform my friends, that I can’t contact them for a while.";
                case 2 : return"Goodbye, my phone. You have served me well.";
            }
            return null;
        }
        
        //Book
        public static string ReadBookMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"I guess reading a book is not a bad thing.";
                case 1: return"They say that book is the window of the world, I may not learn much but it’s better than not learning at all.";
                case 2 : return"I feel like I have broaden my horizons and slightly change my perspective.";
            }
            return null;
        }
        
        public static string SellBookMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"Forgive me. I have to sell this book, ‘cause I need money.";
                case 1: return"Some books get sacrificed for a greater cause.";
                case 2 : return"This book was an incredible read. I hope that anyone who bought it can appreciate it as well.";
            }
            return null;
        }
        
        //Door
        public static string JoggingMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"I need to stretch my muscles, staying at home all the time is not good for my health.";
                case 1: return"Jogging sure gives me some peace of mind.";
                case 2 : return"Nothing beats a good run.";
            }
            return null;
        }
        
        public static string BuyFoodMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"I need food to life.";
                case 1: return"Food is the essential thing to have right now.";
                case 2 : return"A surplus for my stomach, but deficit on my money.";
            }
            return null;
        }
        
        //Refrigerator
        public static string EatMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"*munch munch much* *glug glug glug* My throat is in heaven right now.";
                case 1: return"*munch munch much* *glug glug glug I better treasure the time I’m eating food because I might not eat for a while.";
                case 2 : return"Hmmm…. the best feeling in the world is a full stomach.";
            }
            return null;
        }
        
        public static string ThrowFoodMonolog(int rnd)
        {
            switch (rnd)
            {
                case 0: return"It's too bad if you have to throw it away.";
                case 1: return"May god forgive me, for I have to throw away my food.";
                case 2 : return"A wasted food means wasted money. *sigh*";
            }
            return null;
        }
        
        //Adding
        public const string LockRepairMonolog_1 = "I’m broke. I need money.";
        public const string LockRepairMonolog_2 = "";
        
        public static string FoodStockMonolog(int rnd,int value)
        {
            switch (rnd)
            {
                case 0 : return $"I have {value} food right now";
                case 1 : return $"Some {value} food left in the fridge.”";
                case 2 : return $"These food looks delicious. But, I must conserve my food. Because I have {value} food left in the fridge";
            }
            return null;
        }
        
        public static string BookStockMonolog(int rnd,int value)
        {
            switch (rnd)
            {
                case 0 : return $"I have {value} book right now";
                case 1 : return $"I have {value} book to read or sell.”";
                case 2 : return $"This {value} book is all I have now.";
            }
            return null;
        }
        public static string CheckMoneyMonolog(int rnd,int value)
        {
            switch (rnd)
            {
                case 0 : return $"I have ${value} left in my wallet";
                case 1 : return $"“I only have ${value} in my wallet.”";
                case 2 : return $"Got to save money because I only have ${value} left.";
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