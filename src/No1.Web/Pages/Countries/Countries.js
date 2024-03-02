$(function () {
    var l = abp.localization.getResource('No1');

    //$('#AddNewCountryButton').click(function (e) {
    //    e.preventDefault();
    //    window.location.href = '/Countries/Add';
    //});

    //$('#NewButton').click(function (e) {
    //    e.preventDefault();
    //    $('#createModal').modal('show');
    //});


    var countriesTable = $('#CountriesTable').DataTable({
        serverSide: false,
        paging: false,
        ordering: true,
        order: [[0, "asc"]],
        searching: true,
        scrollX: false,
        info: false,
        ajax: {
            url: 'https://localhost:44370/api/app/country/countries',
            type: 'GET',
            dataType: 'json',
            dataSrc: ''
        },
        columnDefs: [
            {
                target: 0,
                title: l('Name'),
                data: 'name'
                //className: 'text-center'
            },
            {
                width: '100px',
                targets: 1,
                title: l('Actions'),
                data: null,
                render: function (data, type, row) {
                    var viewIcon = '<i class="fa fa-eye view-action" title="' + l('View') + '" data-id="' + data.id + '"></i>';
                    var editIcon = '<i class="fa fa-edit edit-action" title="' + l('Edit') + '"></i>';
                    var deleteIcon = '<i class="fa fa-trash delete-action" title="' + l('Delete') + '" data-id="' + data.id + '"></i>';
                    var separator = '&nbsp;&nbsp;&nbsp;';
                    return viewIcon + separator + editIcon + separator + deleteIcon;
                },
                orderable: false
            }
        ]
    });

    var countriesAllTable = $('#CountriesAllTable').DataTable({
        serverSide: false,
        paging: false,
        ordering: true,
        order: [[0, "asc"]],
        searching: true,
        scrollX: false,
        info: false,
        ajax: {
            url: 'https://localhost:44370/api/app/country/countries',
            type: 'GET',
            dataType: 'json',
            dataSrc: ''
        },
        columns: [
            { title: l('Name'), data: 'name' },
            {
                title: 'Translations', data: 'translations',
                render: function (data) {
                    return data.map(function (translation) {
                        return `${translation.name} (${translation.language})`;
                    }).sort(function (a, b) {
                        const languageA = a.match(/\(([^)]+)\)/)[1];
                        const languageB = b.match(/\(([^)]+)\)/)[1];
                        return languageA.localeCompare(languageB);
                    }).join(', ');
                }
            },
            {
                width: '600px',
                title: 'Cities', data: 'cities',
                render: function (data) {
                    return data.map(function (city) {
                        return city.name;
                    }).sort().join(', ');
                }
            }
        ]
    });

    var citiesTable = $('#CitiesTable').DataTable({
        serverSide: false,
        paging: true,
        ordering: true,
        order: [[0, "asc"]],
        searching: true,
        scrollX: false,
        info: true,
        ajax: {
            url: 'https://localhost:44370/api/app/country/cities',
            type: 'GET',
            dataType: 'json',
            dataSrc: ''
        },
        columns: [
            { title: l('Name'), data: 'name' },
            { title: l('PostCode'), data: 'postCode' },
            { title: l('Latitude'), data: 'latitude' },
            { title: l('Longitude'), data: 'longitude' },
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





    $('#CountriesTable').on('click', '.view-action', function () {
        var countryId = $(this).data('id');

        $.ajax({
            url: 'https://localhost:44370/api/app/country/' + countryId + '/country',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                $('#viewModal').modal('show');

                var selectedCountryName = response.name;
                $('#viewModal .modal-title').text(selectedCountryName);

                var modalBody = $('#viewModal .modal-body');
                modalBody.empty();

                // Gradovi
                var citiesList = $('<ul></ul>');
                var cityNames = response.cities.map(function (city) {
                    return city.name;
                });

                cityNames.sort();
                var citiesText = cityNames.join(', ');

                citiesList.append('<li><strong>Cities:</strong></li>');
                citiesList.append('<li>' + citiesText + '</li');

                // Prijevodi (translations)
                var translationsList = $('<ul></ul>');
                var translationNames = response.translations.map(function (translation) {
                    return translation.name;
                });

                translationNames.sort();
                var translationsText = translationNames.join(', ');

                translationsList.append('<li><strong>Translations:</strong></li>');
                translationsList.append('<li>' + translationsText + '</li');

                // Dodajte obje liste u modalno tijelo
                modalBody.append(citiesList);
                modalBody.append(translationsList);
            },
            error: function () {
                // Obrada grešaka
            }
        });
    });

    $('#CountriesTable').on('click', '.delete-action', function () {
        var countryId = $(this).data('id');

        Swal.fire({
            title: 'Delete Country',
            text: 'Are you sure you want to delete this country?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:44370/api/app/country/' + countryId + '/country',
                    type: 'DELETE',
                    success: function (response) {
                        Swal.fire('Deleted!', 'The country has been deleted.', 'success');
                        Swal.fire({
                            icon: 'success',
                            title: 'Deleted!',
                            text: 'The country has been deleted.',
                            showConfirmButton: false,
                            timer: 2000
                        });
                        //var countriesTable = $('#CountriesTable').DataTable();
                        countriesTable.ajax.reload();
                        //var countriesAllDataTable = $('#CountriesAllDataTable').DataTable();
                        countriesAllTable.ajax.reload();
                    },
                    error: function () {
                        Swal.fire('Error', 'An error occurred while deleting the country.', 'error');
                    }
                });
            }
        });
    });


    $('.delete-button').click(function (e) {
        var countryId = $(this).data('country-id');
        Swal.fire({
            title: 'Delete Country',
            text: 'Are you sure you want to delete this country?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:44370/api/app/country/' + countryId + '/country',
                    type: 'DELETE',
                    success: function (response) {
                        Swal.fire('Deleted!', 'The country has been deleted.', 'success');
                        Swal.fire({
                            icon: 'success',
                            title: 'Deleted!',
                            text: 'The country has been deleted.',
                            showConfirmButton: false,
                            timer: 2000
                        });
                        //var countriesTable = $('#CountriesTable').DataTable();
                        countriesTable.ajax.reload();
                        //var countriesAllDataTable = $('#CountriesAllDataTable').DataTable();
                        countriesAllDataTable.ajax.reload();
                    },
                    error: function () {
                        Swal.fire('Error', 'An error occurred while deleting the country.', 'error');
                    }
                });
            }
        });
    });




    // START --- Create Modal ---
    var createModal = new abp.ModalManager('/Countries/CreateModal');

    $('#CreateButtonTop').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#CreateButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    createModal.onResult(function () {
        countriesTable.ajax.reload();
        countriesAllTable.ajax.reload();
    });
    // END --- Create Modal ---
});