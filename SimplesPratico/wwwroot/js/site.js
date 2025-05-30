﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {
    getDatatable('#table-clientes');
    getDatatable('#table-funcionario');

    $('.btn-total-clientes').click(function () {
        var funcionarioId = $(this).attr('funcionario-id');

        $.ajax({
            type:'GET',
            url: '/Funcionarios/ListarClientesPorFuncionarioId/' + funcionarioId,
            success: function (result) {
                $("#listaClientesFuncionario").html(result);
                $('#modalClientesFuncionario').modal('show');
                if ($('#table-contatos-usuario tbody tr').length > 0 &&
                    $('#table-contatos-usuario tbody tr td[colspan]').length === 0) {
                    getDatateble('#table-contatos-usuario');
                }
            }
        });
 
    });
});

function getDatatable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
};

$('.close-alert').click(function() {
    $('.alert').fadeOut();
});

