using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class Utilizadores
    {
        public Utilizadores() {

         this.ListaNoticias = new HashSet<Noticias>();
        }


        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio")]
        [StringLength(40, ErrorMessage = "O {0} nao deve ter mais de {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio")]
        
        [StringLength(64,MinimumLength = 4, ErrorMessage = "O {0} nao deve ter menos de {2}, nem mais de {1} caracteres")]
        public string Email { get; set; }


        public ICollection<Noticias> ListaNoticias {get;set;}

    }
}
