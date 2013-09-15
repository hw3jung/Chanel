using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Models;

namespace BookSpade.Revamped.Utilities
{
    public class QueueWorker
    {
        public static void AddPost(Post newPost)
        {
            BookQueueSingleton.Instance.Add(newPost);
        }

        public static void ProcessPosts()
        {
            // GetConsumingEnumerable returns the enumerator for the  
            // underlying collection.
            foreach (Post post in BookQueueSingleton.Instance.GetConsumingEnumerable())
            {
                Matchmaker.Match(post);
            }
        }
    }
}