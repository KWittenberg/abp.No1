$(function () {
    var l = abp.localization.getResource('No1');

    var cityName = $('#OpenWeatherData').data('cityName');
    var unit = $('#OpenWeatherData').data('unit');

    $.ajax({
        // url: 'https://localhost:44370/api/app/open-weather/weather-by-city-name?cityName=&unit=0',
        url: 'https://localhost:44370/api/app/open-weather/weather-by-city-name?cityName=' + cityName + '&unit=' + unit,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Prikazivanje podataka u elementu OpenWeatherData
            var openWeatherDataElement = $('#OpenWeatherData');
            openWeatherDataElement.html(
                '<p>Name: ' + data.name + '</p>' +
                '<p>Main: ' + data.main + '</p>' +
                '<p>Description: ' + data.description + '</p>' +
                '<p>Icon: ' + data.icon + '</p>' +
                '<p>Latitude: ' + data.latitude + '</p>' +
                '<p>Longitude: ' + data.longitude + '</p>' +
                '<p>Timezone: ' + data.timezone + '</p>' +
                '<p>Cod: ' + data.cod + '</p>' +
                '<p>Temp: ' + data.temp + '</p>' +
                '<p>FeelsLike: ' + data.feelsLike + '</p>' +
                '<p>TempMin: ' + data.tempMin + '</p>' +
                '<p>TempMax: ' + data.tempMax + '</p>' +
                '<p>Pressure: ' + data.pressure + '</p>' +
                '<p>Humidity: ' + data.humidity + '</p>' +
                '<p>SeaLevel: ' + data.seaLevel + '</p>' +
                '<p>GrndLevel: ' + data.grndLevel + '</p>' +
                '<p>WindSpeed: ' + data.windSpeed + '</p>' +
                '<p>WindDeg: ' + data.windDeg + '</p>' +
                '<p>WindGust: ' + data.windGust + '</p>'
            );
        },
        error: function (xhr, status, error) {
            console.log("Error:", error);
        }
    });


    var openWeatherTable = $('#OpenWeatherTable').DataTable({
        serverSide: false,
        paging: false,
        ordering: true,
        order: [[0, "asc"]],
        searching: false,
        scrollX: false,
        info: false,
        ajax: {
            url: 'https://localhost:44370/api/app/open-weather/weather-by-city-name?cityName=Zagreb&unit=0',
            type: 'GET',
            dataType: 'json',
            dataSrc: '',
            error: function (xhr, status, error) {
                console.log("Error:", error);
            }
        },
        columns: [
            { title: 'Id', data: 'id' },
            { title: 'Name', data: 'name' },
            { title: 'Main', data: 'main' },
            { title: 'Description', data: 'description' },
            { title: 'Icon', data: 'icon' },
            { title: 'Latitude', data: 'latitude' },
            { title: 'Longitude', data: 'longitude' },
            { title: 'Timezone', data: 'timezone' },
            { title: 'Cod', data: 'cod' },
            { title: 'Temperature', data: 'temp' },
            { title: 'Feels Like', data: 'feelsLike' },
            { title: 'Temp Min', data: 'tempMin' },
            { title: 'Temp Max', data: 'tempMax' },
            { title: 'Pressure', data: 'pressure' },
            { title: 'Humidity', data: 'humidity' },
            { title: 'Sea Level', data: 'seaLevel' },
            { title: 'Ground Level', data: 'grndLevel' },
            { title: 'Wind Speed', data: 'windSpeed' },
            { title: 'Wind Degree', data: 'windDeg' },
            { title: 'Wind Gust', data: 'windGust' }
        ]
    });





    // START --- Update City Modal ---
    var updateModal = new abp.ModalManager('/OpenWeather/UpdateModal');

    $('#Update').click(function (e) {
        e.preventDefault();
        updateModal.open();
    });

    updateModal.onResult(function () {
        //categoriesTable.ajax.reload();
        //toastr.options.positionClass = 'toast-top-center';
        abp.notify.success(' City Name Updated!');
    });
    // END --- Update City Modal ---
});