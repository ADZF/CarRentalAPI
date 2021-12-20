function InsertDataCustomer() {
    var obj = new Object();
    obj.NIK = $('#nik').val();
    obj.FirstName = $('#firstName').val();
    obj.LastName = $('#lastName').val();
    obj.Email = $('#email').val();
    obj.Phone = $('#phone').val();
    obj.Gender = $('#gender').val();
    obj.Address = $('#address').val();
    obj.BirthDate = $('#birthDate').val();
    console.log(obj);
    $.ajax({
        url: "/Customers/AddCustomer",
        'type': 'POST',
        'data': { entity: obj },
        'dataType': 'json',
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: 'Success!',
            text: 'Data successfully Added'
        });
        mytable.ajax.reload();
        $("#modalData").modal("hide");
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops..',
            text: 'Failed to Add Data'
        });
    });
};

function InsertDataRental() {
    var obj = new Object();
    obj.CustomerId = $('#customerName').val();
    obj.CarId = $('#carName').val();
    obj.RentDate = $('#rentDate').val();
    obj.ReturnDate = $('#returnDate').val();
    obj.Status = 0;
    console.log(obj);
    $.ajax({
        url: "/Rentals/AddRental",
        'type': 'POST',
        'data': { entity: obj },
        'dataType': 'json',
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: 'Success!',
            text: 'Data successfully Added'
        });
        $("#modalData").modal("hide");
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops..',
            text: 'Failed to Add Data'
        });
    });
};


$(document).ready(function () {
    $('#tableUser').DataTable({

        'ajax': {
            'url': "/Customers/GetAll",
            'order': [[0, 'asc']],
            'dataSrc': ''
        },

        'columns': [
            {
                data: 'no', name: 'id', render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "nik" },
            {
                "data": "",
                'render': function (data, type, row, meta) {
                    return row['firstName'] + " " + row['lastName'];

                }
            },
            { "data": "birthDate" },
            {
                "data": "gender",
                'render': function (data, type, row, meta) {
                    if (row['gender'] == 0) {
                        return "Male"
                    }
                    return "Female"
                }
            },
            {
                "data": "phone",
                render: function (data, type, row, meta) {
                    if (row['phone'].search(0) == 0) {
                        return row['phone'].replace('0', '+62');
                    } else {
                        return row['phone'];
                    }
                }
            },
            { "data": "email" },
            { "data": "address" },
            {
                "data": "",
                'render': function (data, type, row, meta) {
                    return `<td scope=" row">  <a class="btn btn-warning btn-sm text-light" data-url=""  onclick="getDetailUser('${row.nik}')" title="Detail"><i class="fa fa-info-circle"></i> </a></td>
                                    <td scope=" row"><a class="btn btn-primary btn-sm text-light" data-url=""  onclick="getUser('${row.nik}')"  title="Update"><i class="far fa-edit"></i></a></td>
                                  <td scope=" row"> <button type="button" class="btn btn-danger btn-sm text-light"  onclick="DeleteUser('${row.nik}')" title="Delete"> <i class="fas fa-trash-alt"></i></button></td>`;
                }
            }
        ]
    });
})

$(document).ready(function () {
    $('#tableRental').DataTable({

        'ajax': {
            'url': "/Rentals/GetAll",
            'order': [[0, 'asc']],
            'dataSrc': ''
        },
        
        'columns': [
            {
                data: 'no', name: 'id', render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "orderId"},
            { "data": "rentDate" },
            { "data": "returnDate" },
            { "data": "customerId" },
            { "data": "carId" },
            { "data": "status" },         
            {
                "data": "",
                'render': function (data, type, row, meta) {
                    return `<td scope=" row">  <a class="btn btn-warning btn-sm text-light" data-url=""  onclick="getDetailRental('${row.orderId}')" title="Detail"><i class="fa fa-info-circle"></i> </a></td>
                                    <td scope=" row"><a class="btn btn-primary btn-sm text-light" data-url=""  onclick="getUser('${row.orderId}')"  title="Update"><i class="far fa-edit"></i></a></td>`;
                }
            }
        ]
    });
})

function getDetailUser(nik) {
    console.log(nik)
    $.ajax({
        url: "/Customers/GetUser/" + nik,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            var tanggal = result[0].birthDate.substr(0, 10);
            $('#nik').val(result[0].nik);
            $('#firstName').val(result[0].firstName);
            $('#lastName').val(result[0].lastName);
            $('#email').val(result[0].email);
            $('#phone').val(result[0].phone);
            $('#gender').val(result[0].gender);
            $('#address').val(result[0].address);
            $('#birthDate').val(tanggal);
            $('#modalData').modal('show');
            $('#btnUpdateUser').hide();
            $('#btnAddUser').hide();
            $('#hideRow').hide();
            $('#nik').prop('disabled', true);;
            $('#firstName').prop('disabled', true);;
            $('#lastName').prop('disabled', true);;
            $('#phone').prop('disabled', true);;
            $('#gender').prop('disabled', true);;
            $('#birthDate').prop('disabled', true);;
            $('#tanggal').prop('disabled', true);;
            $('#address').prop('disabled', true);;   
            $('#email').prop('disabled', true);;
        },
        error: function (errormessage) {
            /*alert(errormessage.responseText);*/
            swal.fire({
                title: "Failed",
                text: "Data not found",
                icon: "error"
            }).then(function () {
                window.location = "https://localhost:44344/auth/User";
            });
        }
    });
    return false;
}

function getDetailRental(id) {
    console.log(id)
    $.ajax({
        url: "/Rentals/GetidRental/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            $('#customerName').val(result.customerId);
            $('#carName').val(result.CarId);
            $('#rentDate').val(result.RentDate);
            $('#returnDate').val(result.ReturnDate);
            $('#modalRental').modal('show');
            $('#btnUpdateUser').hide();
            $('#btnAddUser').hide();
            $('#hideRow').hide();
            $('#customerName').prop('disabled', true);;
            $('#carName').prop('disabled', true);;
            $('#rentDate').prop('disabled', true);;
            $('#returnDate').prop('disabled', true);;
            $('#modalRental').modal('show');

        },
        error: function (errormessage) {
            /*alert(errormessage.responseText);*/
            swal.fire({
                title: "Failed",
                text: "Data not found",
                icon: "error"
            }).then(function () {
            });
        }
    });
    return false;
}

function getUser(nik) {
    console.log(nik)
    $.ajax({
        url: "/Customers/GetUser/" + nik,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            var tanggal = result[0].birthDate.substr(0, 10);
            $('#nik').val(result[0].nik);
            $('#firstName').val(result[0].firstName);
            $('#lastName').val(result[0].lastName);
            $('#email').val(result[0].email);
            $('#phone').val(result[0].phone);
            $('#gender').val(result[0].gender);
            $('#address').val(result[0].address);
            $('#birthDate').val(tanggal);
            $('#modalData').modal('show');
            $('#btnUpdateUser').show();
            $('#btnAddUser').hide();
            $('#hireDate2').show();
            $('#hireDate1').hide();
            $('#hideRow').hide();
            $('#nik').prop('disabled', true);;
            $('#tanggal').prop('disabled', true);;
        },
        error: function (errormessage) {
            /*alert(errormessage.responseText);*/
            Swal.fire({
                icon: 'error',
                title: 'Oops..',
                text: 'Data no found'
            });
        }
    });
    return false;
}
function UpdateUser() {
    var NIK = $('#nik').val();
    var obj = new Object();
    obj.NIK = $('#nik').val();
    obj.FirstName = $('#firstName').val();
    obj.LastName = $('#lastName').val();
    obj.Email = $('#email').val();
    obj.Phone = $('#phone').val();
    obj.Gender = $('#gender').val();
    obj.Address = $('#address').val();
    obj.BirthDate = $('#birthDate').val();
    console.log(obj);
    $.ajax({
        url: "/Customers/Put/" + nik,
        'type': 'PUT',
        'data': { id: NIK, entity: obj },
        'dataType': 'json',
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: 'Success!',
            text: 'Data successfully Added'
        });
        $('#tableUser').DataTable().ajax.reload();
        $("#modalData").modal("hide");
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops..',
            text: 'Failed to Add Data'
        });
    });
};

function DeleteUser(id) {
    console.log(id);
    Swal.fire({
        title: 'Are you sure want to delete this User?',
        text: "you won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, Delete it!'
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: "/Customers/Delete/" + id,
                    type: "Delete",
                    /*  contentType: "application/json;charset=UTF-8",
                      dataType: "json",*/
                    success: function (result) {
                        //mytable.ajax.reload();
                        return Swal.fire(
                            'Delete Successfull',
                            'User Data Deleted!',
                            'success',
                        ).then(function () {
                            window.location = "https://localhost:44371/Auth/Customer";
                        });
                    },

                    error: function (errormessage) {
                        alert(errormessage.responseText);
                        return Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Delete not successful',
                        });
                    }
                });
            } else {
                swal("Failed to delete data");
            }
        });
}

$.ajax({
    url: "https://localhost:44343/API/Customers",
    success: function (result) {
       var optionName = `<option value="" >---Choose Customer---</option>`;
            $.each(result, function (key, val) {
                optionName += `
                            <option value="${val.nik}">${val.firstName} ${val.lastName}</option>`;
            });
        $('#customerName').html(optionName);
    }
});

$.ajax({
    url: "https://localhost:44343/API/Cars",
    success: function (result) {
        var optionCar = `<option value="" >---Choose Car---</option>`;
        $.each(result, function (key, val) {
            optionCar += `
                            <option value="${val.carId}">${val.carName}</option>`;
        });
        $('#carName').html(optionCar);
    }
});