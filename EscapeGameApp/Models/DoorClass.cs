using System;

namespace EscapeGame
{
    public abstract class Door
    {
        protected bool isLocked = true;

        public virtual void Unlock()
        {
            if (isLocked)
            {
                Console.WriteLine("You unlocked the door.");
                isLocked = false;
            }
            else
            {
                Console.WriteLine("The door is already unlocked.");
            }
        }

        public bool IsLocked()
        {
            return isLocked;
        }
    }

    public class KitchenDoor : Door { }
    public class LibraryDoor : Door { }
    public class HiddenDoor : Door
    {
        public bool IsHidden { get; private set; } = true;

        public void Reveal()
        {
            Console.WriteLine("You found the hidden door!");
            IsHidden = false;
        }
    }
    public class FrontDoor : Door { }
}