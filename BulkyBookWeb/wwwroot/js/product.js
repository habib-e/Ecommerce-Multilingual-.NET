

var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}


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
                    dataTable.ajax.reload(); //refresh the data table
                    toastr.success(data.message);
                }
            })
        }
    })
}


//var js = jQuery.noConfict(true);
//$(document).ready(function (){
    
//    LoadListing();
//});

//function LoadListing() {
//    var empdata = [];
//    $.ajax({
//        //type : "GET",
//        url: "/admin/product/getall",
//        async: false,
//        success: function (data) {
//            //$('#tblData').html(data);
//            //console.log(data);
//            $.each(data, function (key, val) {
//                //console.log(val);
//                empdata.push([val.title, val.isbn, val.listPrice, val.author]);
//            })
//        }
//    });
//    $('#tblData').DataTable({
//        data: empdata
//    });
//}