using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class Imagens
    {
        public Imagens(){
           this.ListaNI = new HashSet<NI>();
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        [StringLength(64, ErrorMessage = "O {0} nao deve ter menos de {1}, nem mais de {2} caracteres")]
        public string Legenda { get; set; }


        public ICollection<NI> ListaNI {get;set;}
    }
}
