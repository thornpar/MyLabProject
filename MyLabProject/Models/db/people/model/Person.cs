using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyLabProject.Models.db.people.model
{
    public class Person
    {
        public Person()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, object> DynamicProperties { get; set; }

        //public virtual IList<PersonDynamics> DynamicFields { get; set; }

        /*private IDictionary<string, object> tempDictionary { get; set; }
        public virtual IDictionary<string, object> DynamicProperties
        {
            get
            {
                tempDictionary = new Dictionary<string, object>();
                foreach(var e in DynamicFields)
                {
                    tempDictionary.Add(e.Field, e.Value);
                }
                return tempDictionary;
            }
            set
            {
                tempDictionary = value;
            }
        }*/
    }
}