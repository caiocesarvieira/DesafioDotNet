namespace DesafioDotNet.Entity
{
    public class Login
    {
        public int Codigo { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

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
    }
}

