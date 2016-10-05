using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyLabProject.Models.db.people.model
{
    public class PersonDynamics
    {
        public PersonDynamics()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public virtual string Field { get; set; }
        public virtual string Value { get; set; }

    }
}