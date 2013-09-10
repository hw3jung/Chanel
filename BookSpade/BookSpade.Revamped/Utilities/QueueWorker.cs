using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Models;

namespace BookSpade.Revamped.Utilities
{
    public class QueueWorker
    {
        public static void RunProducer(Post newPost)
        {
            BookQueueSingleton.Instance.Add(newPost);
        }

        public static void RunConsumer()
        {
            // GetConsumingEnumerable returns the enumerator for the  
            // underlying collection.
            foreach (var post in BookQueueSingleton.Instance.GetConsumingEnumerable())
            {
                
            }
            
            //Console.WriteLine("Press any key to exit");
        }
    }
}