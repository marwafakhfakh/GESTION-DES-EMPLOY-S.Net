﻿using System.ComponentModel.DataAnnotations;

namespace atelier1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required,StringLength(50 , ErrorMessage ="Taille max 50 characters")]
        public string Name { get; set; }
        [Required]
        public string Departement { get; set; }
        [Range(200,5000)]
        public int Salary { get; set; }
    }
}
