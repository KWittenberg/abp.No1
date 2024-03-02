$(function () {
    var l = abp.localization.getResource('No1');

    $('#NewButton').click(function (e) {
        e.preventDefault();
        $('#createModal').modal('show');
    });


    var rolesTable = $('#RolesTable').DataTable({
        serverSide: false,
        paging: false,
        ordering: true,
        order: [[0, "asc"]],
        searching: true,
        scrollX: false,
        info: false,
        ajax: {
            url: 'https://localhost:44370/api/app/application-user/roles',
            type: 'GET',
            dataType: 'json',
            dataSrc: ''
        },
        columnDefs: [
            {
                target: 0,
                title: l('Name'),
                data: 'name'
            },
            {
                target: 1,
                className: 'text-center',
                title: l('Id'),
                data: 'id'
            },
            {
                target: 2,
                title: '<div style="text-align: right;">' + l('Actions') + '</div>',
                data: null,
                render: function (data, type, row) {
                    var editIcon = '<i class="fa fa-edit edit-action" title="' + l('Edit') + '"></i>';
                    var deleteIcon = '<i class="fa fa-trash delete-action" title="' + l('Delete') + '" data-id="' + data.id + '"></i>';
                    var separator = '&nbsp;&nbsp;&nbsp;';
                    return '<div style="text-align: right;">' + editIcon + separator + deleteIcon + '</div>';
                },
                orderable: false
            }
        ]
    });

    $('#RolesTable').on('click', '.delete-action', function () {
        var roleId = $(this).data('id');

        Swal.fire({
            title: 'Delete Role',
            text: 'Are you sure you want to delete this role?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:44370/api/identity/roles/' + roleId,
                    type: 'DELETE',
                    success: function (response) {
                        Swal.fire('Deleted!', 'The role has been deleted.', 'success');
                        Swal.fire({
                            icon: 'success',
                            title: 'Deleted!',
                            text: 'The role has been deleted.',
                            showConfirmButton: false,
                            timer: 2000
                        });
                        var rolesTable = $('#RolesTable').DataTable();
                        rolesTable.ajax.reload();
                    },
                    error: function () {
                        Swal.fire('Error', 'An error occurred while deleting the role.', 'error');
                    }
                });
            }
        });
    });
});