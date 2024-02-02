using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cities.API.Infrastructure.Models
{
    [Table("CITIES")]
    public class City(string name, string state)
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required, MaxLength(255, ErrorMessage = "O nome deve ter no máximo 255 caracteres")]
        public string Name { get; set; } = name;

        [Required, MaxLength(255, ErrorMessage = "O nome do estado deve ter no máximo 255 caracteres")]
        public string State { get; set; } = state;
    }
}
