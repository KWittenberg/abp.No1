$(function () {
    var l = abp.localization.getResource('No1');

    $('#NewButton').click(function (e) {
        e.preventDefault();
        $('#createModal').modal('show');
    });


    var usersTable = $('#UsersTable').DataTable({
        serverSide: false,
        paging: false,
        ordering: true,
        order: [[0, "asc"]],
        searching: true,
        scrollX: false,
        info: false,
        ajax: {
            url: 'https://localhost:44370/api/app/application-user',
            type: 'GET',
            dataType: 'json',
            dataSrc: ''
        },
        columns: [
            {
                title: 'Avatar',
                data: 'avatarUrl',
                render: function (data) {
                    if (data) {
                        return '<img src="' + data + '" alt="Avatar" width="30" height="30" />';
                    } else {
                        return 'No Avatar';
                    }
                }
            },
            { title: 'Username', data: 'userName' },
            {
                title: 'Roles',
                data: 'roleIds',
                render: function (data) {
                    var roleNames = data.map(function (roleId) {
                        return getRoleNameById(roleId);
                    });
                    var badgeHtml = roleNames.map(function (roleName) {
                        return '<span class="badge text-bg-primary">' + roleName + '</span>';
                    });
                    return badgeHtml.join(' ');
                }
            },
            { title: 'FirstName', data: 'firstName' },
            { title: 'LastName', data: 'lastName' },
            { title: 'Email', data: 'email' },
            {
                title: '<div style="text-align: right;">' + l('Actions') + '</div>',
                data: null,
                render: function (data, type, row) {
                    var viewIcon = '<i class="fa fa-eye view-action" title="' + l('View') + '" data-id="' + data.id + '"></i>';
                    var editIcon = '<i class="fa fa-edit edit-action" title="' + l('Edit') + '"></i>';
                    var deleteIcon = '<i class="fa fa-trash delete-action" title="' + l('Delete') + '" data-id="' + data.id + '"></i>';
                    var separator = '&nbsp;&nbsp;&nbsp;';
                    return '<div style="text-align: right;">' + viewIcon + separator + editIcon + separator + deleteIcon + '</div>';
                },
                orderable: false
            }
        ]

    });


    $('#UsersTable').on('click', '.view-action', function () {
        var userId = $(this).data('id');
        console.log(userId);
        $.ajax({
            url: 'https://localhost:44370/api/app/application-user/user/' + userId,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                $('#viewModal').modal('show');

                var selectedUser = '<strong>' + response.userName + '</strong>';
                $('#viewModal .modal-title').html(selectedUser);

                $('#avatar').attr('src', response.avatarUrl);
                $('#userName').text(response.userName);


                var roleIds = response.roleIds;
                var roleNames = roleIds.map(function (roleId) {
                    return getRoleNameById(roleId);
                });
                roleNames = roleNames.filter(function (roleName) {
                    return roleName !== undefined;
                });
                var badgeHtml = roleNames.map(function (roleName) {
                    return '<span class="badge text-bg-primary">' + roleName + '</span>';
                });
                $('#roles').html(badgeHtml.join(' '));


                $('#firstName').text(response.firstName);
                $('#lastName').text(response.lastName);
                $('#email').text(response.email);
                $('#dateOfBirth').text(new Date(response.dateOfBirth).toLocaleDateString());
                $('#country').text(response.country);
                $('#postCode').text(response.postCode);
                $('#city').text(response.city);
                $('#street').text(response.street);
                $('#active').text(response.isActive ? 'Yes' : 'No');
            },
            error: function () {
            }
        });
    });

    $('#UsersTable').on('click', '.delete-action', function () {
        var userId = $(this).data('id');

        Swal.fire({
            title: 'Delete User',
            text: 'Are you sure you want to delete this user?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:44370/api/app/application-user/user/' + userId,
                    type: 'DELETE',
                    success: function (response) {
                        Swal.fire('Deleted!', 'The user has been deleted.', 'success');
                        Swal.fire({
                            icon: 'success',
                            title: 'Deleted!',
                            text: 'The user has been deleted.',
                            showConfirmButton: false,
                            timer: 2000
                        });
                        var rolesTable = $('#UsersTable').DataTable();
                        rolesTable.ajax.reload();
                    },
                    error: function () {
                        Swal.fire('Error', 'An error occurred while deleting the user.', 'error');
                    }
                });
            }
        });
    });




    // START --- Register Modal ---
    var registerModal = new abp.ModalManager('/Users/RegisterModal');

    $('#RegisterButton').click(function (e) {
        e.preventDefault();
        registerModal.open();
    });

    registerModal.onResult(function () {
        var rolesTable = $('#UsersTable').DataTable();
        rolesTable.ajax.reload();
    });
    // END --- Register Modal ---



    // START --- Create Modal ---
    var createModal = new abp.ModalManager('/Users/CreateModal');

    $('#NewUserButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    // END --- Create Modal ---

});

function getRoleNameById(roleId) {
    var roleName = null;
    console.log(roleId);
    $.ajax({
        url: 'https://localhost:44370/api/identity/roles/' + roleId,
        type: 'GET',
        dataType: 'json',
        async: false,
        success: function (response) {
            roleName = response.name;
        },
        error: function () {
            console.error('Greška prilikom dohvaćanja imena role za roleId: ' + roleId);
        }
    });
    return roleName;
}