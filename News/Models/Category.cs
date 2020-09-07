using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryNews { get; set; }
        public IEnumerable<FakeNews> FakeNews { get; set; }
    }
}