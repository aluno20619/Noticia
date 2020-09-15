using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 

namespace News.Models
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