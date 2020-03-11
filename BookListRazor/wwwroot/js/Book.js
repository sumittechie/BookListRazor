﻿
    var datatable;

    $(document).ready(function () {
        loadData();
    });

    function loadData() {
        datatable = $('#DT_load').DataTable({
            "ajax": {
                "url": "/api/book",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "name", "width": "30%" },
                { "data": "author", "width": "30%" },
                { "data": "isbn", "width": "25%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `
                        <div class="text-center">
                            <a href="/BookList/Upsert?id=${data}" class="btn btn-sm btn-success text-white">Edit</a>    
                            <a class="btn btn-sm btn-danger text-white" onclick="Delete('/api/book?id=${data}')">Delete</a>
                        </div>`;
                    },
                    "width": "15%"
                }
            ],
            "language": {
                "emptyTable": "No book records found"
            },
            "width": "100%"
        });
    }


function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted you will not be able to recover",
        icon: "warning",
        dangerMode: true,
        buttons: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}