$(function () {
    var l = abp.localization.getResource('No1');

    var categoriesTable = $('#CategoriesTable').DataTable({
        serverSide: false,
        paging: false,
        ordering: true,
        order: [[0, "asc"]],
        searching: true,
        scrollX: false,
        info: false,
        ajax: {
            url: 'https://localhost:44370/api/app/category/categories',
            type: 'GET',
            dataType: 'json',
            dataSrc: ''
        },
        columns: [
            { title: 'Name', data: 'name' },
            { title: 'Description', data: 'description' },
            {
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


    // START --- Create Category Modal ---
    var createModal = new abp.ModalManager('/Categories/CreateModal');

    $('#NewCategory').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    createModal.onResult(function () {
        categoriesTable.ajax.reload();
        toastr.options.positionClass = 'toast-top-center';
        abp.notify.success(' Category created successfully!');
    });
    // END --- Create Category Modal ---


    // START --- Delete Category ---
    $('#CategoriesTable').on('click', '.delete-action', function () {
        var categoryId = $(this).data('id');

        Swal.fire({
            title: 'Delete Category',
            text: 'Are you sure you want to delete this category?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:44370/api/app/category/category/' + categoryId,
                    type: 'DELETE',
                    success: function (response) {
                        Swal.fire('Deleted!', 'The category has been deleted.', 'success');
                        Swal.fire({
                            icon: 'success',
                            title: 'Deleted!',
                            text: 'The category has been deleted.',
                            showConfirmButton: false,
                            timer: 2000
                        });
                        categoriesTable.ajax.reload();
                    },
                    error: function () {
                        Swal.fire('Error', 'An error occurred while deleting the category.', 'error');
                    }
                });
            }
        });
    });
    // END --- Delete Category ---
});