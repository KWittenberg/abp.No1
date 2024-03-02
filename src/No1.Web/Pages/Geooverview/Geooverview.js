$(function () {
    var l = abp.localization.getResource('No1');

    var geooverviewTable = $('#GeoOverviewTable').DataTable({
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

});