using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookSpade.Revamped.Models;

namespace BookSpade.Revamped.Utilities
{
    public sealed class BookQueueSingleton : BlockingCollection<Post>
    {
        private static readonly BookQueueSingleton instance = new BookQueueSingleton();

        private BookQueueSingleton() {}

        public static BookQueueSingleton Instance
        {
            get
            {
                return instance;
            }
        }
    }
}