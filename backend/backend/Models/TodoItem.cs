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
        public override bool Equals(object obj)
        {
            return Equals(obj as TodoItem);
        }

        public bool Equals(TodoItem other)
        {
            return other != null &&
                   Id == other.Id &&
                   name == other.name &&
                   complete == other.complete &&
                   order == other.order &&
                   date == other.date &&
                   description == other.description;
                   
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, name, complete, order,date,description);
        }

    }
}
