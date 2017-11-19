$(function () {
    tabela.carregar();
});

$("#btnSalvar").click(function (e) {
    e.preventDefault();

    if (validarPreenchimento()) {
        requisicaoAjax(rootUrl + 'Jogo/Salvar', $('form').serializeArray(),
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


$('table#tblJogos').on('click', '.js-editar', function (e) {

    if (confirm(MSG_09)) {
        var $colunasDataTable = $(this).parent().parent().find('td');

        $('#btnCancelar').removeClass('invisivel');
        $('#hdnCodigo').val($(this).attr('id'));
        $('#txtDescricao').val($($colunasDataTable[0]).text());

        subirScroll();
    }
});

$('table#tblJogos').on('click', '.js-excluir', function (e) {

    if (confirm(MSG_08)) {

        $('#hdnCodigo').val($(this).attr('id'));

        requisicaoAjax(rootUrl + 'Jogo/Excluir', $('form').serializeArray(),
         function () {
             tabela.carregar();
         });
    }
});


var tabela = {
    carregar: function () {
        datatableBase.criarTabela({
            idTabela: 'tblJogos',
            controller: 'Jogo',
            action: 'Listar',
            order: [[0, 'desc']],
            fnAdicionarDados: function (data) {
            },
            columns: [
                { mData: 'Descricao', sTitle: 'Descrição', sWidth: '20%', bSortable: true },
                {
                    mData: null, sTitle: 'Ação', sWidth: '10%', bSortable: false, mRender: function (valor, tipo, objeto) {

                        var retorno = '<a id=' + objeto.Codigo + ' class="js-editar" href="javascript:void(0);" ><img class="iconeDataTables" src="' + rootUrl + 'Content/Imagens/editar.png" alt="Editar Amigo" title="Editar Amigo" ></a>';
                        retorno += '<a id=' + objeto.Codigo + ' class="js-excluir" href="javascript:void(0);" ><img class="iconeDataTables" src="' + rootUrl + 'Content/Imagens/excluir.png" alt="Excluir Amigo" title="Excluir Amigo" ></a>';

                        return retorno;
                    }
                },
            ],

        });
    },
}