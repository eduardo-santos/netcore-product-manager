using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    /*
     * This class represents the Category entity
    */
    public class Category
    {
        [Key]
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Data Criação")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
