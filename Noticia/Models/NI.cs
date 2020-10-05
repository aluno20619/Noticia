using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Classe intermedia entre imagens e noticias
/// </summary>
/// 

namespace Noticia.Models
{
	public class NI
	{
    

        public Noticias Noticias { get; set; }
        public Imagens Imagens { get; set; }



        //[ForeignKey(nameof(Noticiaid))]

        public int Noticiasid { get; set; }

        //[ForeignKey(nameof(Imagemid))]
        public int Imagensid { get; set; }

    }
}