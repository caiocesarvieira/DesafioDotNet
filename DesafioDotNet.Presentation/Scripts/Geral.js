function validarPreenchimento() {
    var valido = true;
    $(".campoInvalido").removeClass("campoInvalido");

    $(".js-obrigatorio").each(function () {

        if ($(this).val() == "") {
            var $label = $(this).parent().parent().find('label')
            $(this).addClass("campoInvalido");
            $label.addClass('campoInvalido');
            valido = false;
        }
    });

    if (!valido)
        alert("Preencha os campos obrigatórios.");

    return valido;
};

function limparCampos() {

    $("input[type=text], select, input[type=hidden]").each(function () {
        $(this).val("");
    });
}


function requisicaoAjax(urlMetodo, parametros, funcSucesso, isAsync) {
    $.ajax({
        url: urlMetodo,
        data: parametros,
        type: 'post',
        cache: false,
        async: isAsync == undefined || isAsync,
        dataType: 'json',
        success: function (retorno) {

            if (retorno.IsValido) {
                if (retorno.Mensagem)
                    alert(retorno.Mensagem);
                if (funcSucesso != undefined)
                    funcSucesso(retorno);
            }
            else {
                if (retorno.Mensagem) {
                    alert(retorno.Mensagem);
                    subirScroll();
                }
            }

        }
    });
};

function subirScroll(idCampo) {
    var top = 0;
    if (idCampo != undefined) {
        top = +$("#" + idCampo).position().top;
        if (!top)
            top = 0;
    }
    $("html, body").animate({ scrollTop: top }, "slow");
}


var datatableBase = {
    criarTabela: function (config) {

        //PARAMETRO OBRIGATORIO
        return $('#' + config.idTabela).DataTable({
            destroy: true,
            serverSide: config.serverSide == undefined || config.serverSide,
            sPaginationType: 'full_numbers',
            language: {
                lengthMenu: 'Mostrando _MENU_ registros por página',
                //PARAMETRO OBRIGATORIO
                zeroRecords: config.msgNenhumRegistroEncontrado == undefined ? "Nenhum registro encontrado." : config.msgNenhumRegistroEncontrado,
                info: 'REGISTROS ENCONTRADOS: _TOTAL_',
                infoEmpty: '',
                infoFiltered: '',
                paginate: {
                    first: '←',
                    last: '→',
                    next: '»',
                    previous: '«'
                },
                thousands: '.',
                decimal: ','
            },

            dom: config.dom || '<"table-responsive"t><"bottom"ip>',
            data: config.dados,
            autoWidth: true,
            lengthChange: false,
            scrollX: (config.scrollX == undefined) ? "" : config.scrollX,
            scrollY: (config.scrollY == undefined) ? "" : config.scrollY,
            scrollCollapse: (config.scrollCollapse == undefined) ? "" : config.scrollCollapse,
            //lengthMenu: [2, 5, 10, 25, 50, 75, 100],
            searching: config.searching == undefined || config.searching,
            paginate: config.paginacao == undefined || config.paginacao,
            info: config.info == undefined || config.info,
            //PARAMETRO OPCIONAL
            order: config.order || [[0, 'asc']],
            ordering: config.ordering == undefined || config.ordering,
            ajax: function (data, callback, settings) {
                //PARAMETRO OBRIGATORIO
                if (config.dados == null || config.dados == undefined) {
                    config.fnAdicionarDados(data);
                    $.ajax({
                        dataType: 'json',
                        //PARAMETRO OPCIONAL
                        type: config.method || 'GET',
                        //PARAMETRO OBRIGATORIO
                        url: datatableBase.criarUrl(config.controller, config.action),
                        data: data,
                        success: function (response) {
                            datatableBase.processarRetorno(response, callback, settings, config);
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            if (xhr.status == 500 && xhr.responseJSON) {
                                datatableBase.processarRetorno(xhr.responseJSON, null, settings, config);
                            }
                        }
                    });
                }
            },
            columnDefs: config.columnDefs,
            columns: config.columns,
            fnDrawCallback: function (settings) {
                $(".dataTables_scrollBody").removeAttr('class');

                // Implementação do comportamento de esconder a paginação quando não há registros o bastante para paginar
                var divWrapper = $(settings.nTableWrapper),
                    divPagination = divWrapper.find('.dataTables_paginate');

                if ($.isFunction(config.fnDrawCallback))
                    config.fnDrawCallback(settings);

                if (settings.fnRecordsDisplay() > settings._iDisplayLength)
                    divPagination.show();
                else
                    divPagination.hide();
            },
            rowCallback: config.rowCallback,
            fnCreatedRow: config.fnCreatedRow
        });
    }
    , processarRetorno: function (response, callback, settings, config) {
        datatableBase.exibirMensagem(response, settings, config);
        if (callback != null && response != null && !response.Log) {
            callback(response);
        }
    }
    , exibirMensagem: function (response, settings, config) {
        var $divWrapper = $(settings.nTableWrapper).show();
        var $tableInfoDiv = $('#' + settings.sTableId + 'InfoMessage').hide();
        var $tableErrorDiv = $('#' + settings.sTableId + 'ErrorMessage').hide();
        if (response != null && response.Log != undefined) {
            $divWrapper.hide();
            var mensagemHtml = castMensagens.criarHtmlMensagemLog(response);
            var isTableInModal = $divWrapper.parents('.modal').length > 0;
            if (isTableInModal) {
                $tableErrorDiv.find('.datatables-error-details')
                    .empty()
                    .append(mensagemHtml);
                $tableErrorDiv.show();
            } else {
                castModal.modalErroSistema(mensagemHtml, function () { });
            }
        } else if (response != null && response.recordsTotal == 0) {
            $divWrapper.hide();
            $tableInfoDiv.find('.js-mensagem').text(settings.oLanguage.sEmptyTable);
            $tableInfoDiv.show();
        }
    }
    , criarUrl: function (nomeController, nomeAcao) {
        var urlBase = rootUrl + nomeController;

        if (nomeAcao != undefined || nomeAcao != null) {
            urlBase += '/' + nomeAcao;
        }

        return urlBase;
    }
};