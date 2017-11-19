using System;

namespace DesafioDotNet.Entity
{
    public class PessoaJogo
    {
        public int Codigo { get; set; }
        public DateTime Data { get; set; }

        private Pessoa _pessoa;
        public Pessoa Pessoa
        {
            get
            {
                if (_pessoa == null)
                {
                    _pessoa = new Pessoa();
                }
                return _pessoa;
            }
            set
            { _pessoa = value; }
        }

        private Jogo _jogo;
        public Jogo Jogo
        {
            get
            {
                if (_jogo == null)
                {
                    _jogo = new Jogo();
                }
                return _jogo;
            }
            set
            { _jogo = value; }
        }
    }
}

