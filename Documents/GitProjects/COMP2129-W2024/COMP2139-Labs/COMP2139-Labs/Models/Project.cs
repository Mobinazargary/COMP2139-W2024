﻿using System;
using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Models
{
    public class Project
    {
        //Attributes

        [Key]
        public int projectID { get; set; } //This creates a PRIVATE attribute but public getter and setter for the attribute

        [Required] //"Required" annotation
        public string Name { get; set; }

        public string? Description { get; set; } //the question mark mean this field is nullable (optional) it can be a string or Null.

        [DataType(DataType.Date)]       //This is called an annotation to enforce a certian datatype
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string? Status { get; set; }



        public Project()
        {
        }
    }
}
