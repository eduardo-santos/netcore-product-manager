using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer
{
    /*
     * This class represents the Product entity
    */
    public class Product
    {
        [Key]
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Categoria é obrigatório.")]
        [Display(Name = "Id Categoria")]
        public int IdCategory { get; set; }

        [Required(ErrorMessage = "O campo Preço é obrigatório.")]
        [Display(Name = "Preço")]
        [Range(0.1, 999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Data Criação")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [ForeignKey("IdCategory")]
        [Display(Name = "Categoria")]
        public Category Category { get; set; }
    }
}
