$(function () {
    tabela.carregar();
});

$("#btnSalvar").click(function (e) {
    e.preventDefault();

    if (validarPreenchimento()) {
        requisicaoAjax(rootUrl + 'Emprestimo/Salvar', $('form').serializeArray(),
            function () {
                limparCampos();
                $('#btnCancelar').addClass('invisivel');
                tabela.carregar();
            });
    }

});

$('#btnCancelar').click(function (e) {
    e.preventDefault();
    limparCampos();
    $(this).addClass('invisivel');
});

$('#btnConsultar').click(function (e) {
    e.preventDefault();
    tabela.carregar();
});


$('table#tblEmprestimos').on('click', '.js-editar', function (e) {

    if (confirm(MSG_09)) {

        var codigos = $(this).attr('id').split('|');

        $('#btnCancelar').removeClass('invisivel');
        $('#hdnCodigo').val(codigos[0]);
        $('#ddlAmigo').val(codigos[1]);
        $('#ddlJogo').val(codigos[2]);

        subirScroll();
    }
});

$('table#tblEmprestimos').on('click', '.js-excluir', function (e) {

    if (confirm(MSG_08)) {

        var codigos = $(this).attr('id').split('|');

        $('#hdnCodigo').val(codigos[0]);

        requisicaoAjax(rootUrl + 'Emprestimo/Excluir', $('form').serializeArray(),
          function () {
              tabela.carregar();
          });
    }
});


var tabela = {
    carregar: function () {
        datatableBase.criarTabela({
            idTabela: 'tblEmprestimos',
            controller: 'Emprestimo',
            action: 'Listar',
            order: [[0, 'desc']],
            fnAdicionarDados: function (data) {
                data.nomeAmigo = $('#txtNomeAmigo').val(),
                data.descricaoJogo = $('#txtDescricaoJogo').val()
            },
            columns: [
                {
                    mData: 'Pessoa.Nome', sTitle: 'Amigo', sWidth: '40%', orderable: true, mRender: function (valor, tipo, objeto) {
                        var nome = objeto.Pessoa.Nome;
                        if (objeto.Pessoa.Apelido != null)
                            nome += " (" + objeto.Pessoa.Apelido + ")";
                        return nome;
                    }
                },
                { mData: 'Jogo.Descricao', sTitle: 'Jogo', sWidth: '40%', orderable: true },
                {
                    mData: null, sTitle: 'Ação', sWidth: '20%', orderable: false, mRender: function (valor, tipo, objeto) {

                        var retorno = '<a id=' + objeto.Codigo + '|' + objeto.Pessoa.Codigo + '|' + objeto.Jogo.Codigo + ' class="js-editar" href="javascript:void(0);" ><img class="iconeDataTables" src="' + rootUrl + 'Content/Imagens/editar.png" alt="Editar Amigo" title="Editar Amigo" ></a>';
                        retorno += '<a id=' + objeto.Codigo + '|' + objeto.Pessoa.Codigo + '|' + objeto.Jogo.Codigo + ' class="js-excluir" href="javascript:void(0);" ><img class="iconeDataTables" src="' + rootUrl + 'Content/Imagens/excluir.png" alt="Excluir Amigo" title="Excluir Amigo" ></a>';

                        return retorno;
                    }
                },
            ],

        });
    },
}