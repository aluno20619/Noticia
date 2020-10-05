using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Classe intermédia entre noticias e topicos
/// </summary>
/// 
namespace Noticia.Models
{
	public class NT
	{

        


        public Noticias Noticias { get; set; }
        
        public Topicos Topicos{ get; set; }

        //[ForeignKey(nameof(Noticiaid))]
        public int Noticiasid { get; set; }

        //[ForeignKey(nameof(Topicoid))]
        public int Topicosid { get; set; }

    }
}