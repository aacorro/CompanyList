var dataTable;


$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Companies/GetAll' },
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'address', "width": "20%" },
            { data: 'city', "width": "20%" },
            { data: 'state', "width": "5%" },
            { data: 'postalCode', "width": "10%" },
            {
                data: 'companyId',
                "render": function (data) {
                    return `
                    <div class="col">
                        <a href="/Companies/Details?id=${data}" class="btn btn-primary"><i class="bi bi-search text-primary"></i></a> 
                        <a href="/Companies/Edit?id=${data}" class="btn btn-success"><i class="bi bi-pencil-square text-success"></i></a>

                        <a onClick=Delete('/Companies/Delete/${data}') class="btn btn-danger"><i class="bi bi-trash3 text-danger"></i></a>

                    </div>
                    `
                },
                "width": "25%"
            }
        ]
    });
}

/*<a href="/Companies/Delete?id=${data}" class="btn btn-danger"><i class="bi bi-trash3 text-danger"></i></a>*/


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}

