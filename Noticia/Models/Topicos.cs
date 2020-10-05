using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace Noticia.Models
{
	public class Topicos
	{
		public Topicos()
		{
			this.ListaNT = new HashSet<NT>();
		}

		[Key]
		public int Id { get; set; }
		public string Nome { get; set; }

		public ICollection<NT> ListaNT { get; set; }
	}
}