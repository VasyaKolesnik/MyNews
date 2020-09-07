using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace News.Models
{
    public class NewsContext : DbContext
    {
        public NewsContext() : 
            base("NewsContext")
        { }
        public DbSet<FakeNews> FakeNews { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}