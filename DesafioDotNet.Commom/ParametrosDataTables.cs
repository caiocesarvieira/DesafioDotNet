using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDotNet.Commom
{
    public class ParametrosDataTables
    {
        public DatatableColumn[] columns { get; set; }
        public int draw { get; set; }
        public int length { get; set; }
        public string NomeColunaOrdenacao { get; set; }
        public DatatablesOrder[] order { get; set; }
        public DatatablesSearch search { get; set; }
        public int start { get; set; }
    }

    public class DatatableColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool orderable { get; set; }
        public DatatablesSearch search { get; set; }
        public bool searchable { get; set; }
    }

    public class DatatablesSearch
    {
        public bool regex { get; set; }
        public string value { get; set; }
    }

    public class DatatablesOrder
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class DatatablesResponse
    {
        public object aaData { get; set; }
        public int iErrorCode { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public int iTotalRecords { get; set; }
        public string sEcho { get; set; }
        public string sErrorMessage { get; set; }
    }
}


