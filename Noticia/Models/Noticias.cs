using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class Noticias 
    {



        public Noticias() {
         this.ListaNI = new HashSet<NI>();
         this.ListaNT = new HashSet<NT>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio")]
        public string Titulo { get; set; }

        public string Resumo { get; set; }

        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio")]
        public string Corpo { get; set; }
        public DateTime Data_De_Publicacao{get;set;}
        public bool Visivel{get;set;}

        [ForeignKey(nameof(Utilizadoresid))] 
        public int UtilizadoresidFK { get; set; } 
        public  Utilizadores Utilizadoresid { get; set; } 
        
        public  ICollection<NI> ListaNI {get;set;}
        public  ICollection<NT> ListaNT {get;set;}

    }
}