using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    

    public class TodoItem
    {
    
        public long Id { get; set; }
        public string name { get; set; }
        public int complete { get; set; }
        public int order { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public TodoItem()
        {
        }
        public TodoItem(string name, int complete,int order,DateTime date,string description)
        {
            this.name = name;
            this.complete = complete;
            this.order = order;
            this.date = date;
            this.description = description;
        }

    }
}
