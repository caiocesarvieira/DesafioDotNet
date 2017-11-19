using DesafioDotNet.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioDotNet.Presentation.Models
{
    public class EmprestimoViewModel
    {
        public PessoaJogo Emprestimo { get; set; }

        public List<Jogo> Jogos { get; set; }

        public List<Pessoa> Amigos { get; set; }
    }
}