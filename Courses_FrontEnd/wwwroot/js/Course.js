$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: 'https://localhost:7230/api/Course' },
        "columns": [
            { data: 'courseName', "width": "25%" },
            { data: 'startDate', "width": "15%" },
            { data: 'endDate', "width": "10%" },
            { data: 'status', "width": "20%" },
            { data: 'availabeSeats', "width": "15%" }
        ]
    });
}