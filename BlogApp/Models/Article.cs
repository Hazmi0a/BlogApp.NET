using System;
using Realms;

namespace BlogApp.Models
{
    public class Article : RealmObject
    {
        public String title
        {
            get;
            set;
        }

        public String content
        {
            get;
            set;
        }

        public String img
        {
            get;
            set;
        }

    }
}
