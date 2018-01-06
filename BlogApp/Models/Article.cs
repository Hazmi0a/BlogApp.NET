using System;
using Realms;
using System.Collections.Generic;

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

        public String image_url
        {
            get;
            set;
        }

        // Default constractor
        public Article()
        {

        }
        // Overloading 
        public Article(String title, String content, String image_url)
        {
            this.title = title;
            this.content = content;
            this.image_url = image_url;
        }

        // Overridding ToString
        public override string ToString() {
            return "Title: " + this.title + "\nContent: " + this.content;
        }

    }
}
