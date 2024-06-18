$(function () {
    $('#roleTable').DataTable();

});
function Delete(id) {
    Swal.fire({
        title: "حذف",
        text: "هل بالتاكيد تريد حذف هذه المجموعة؟",
        icon: 'warning',
        showCancelButton : true,
        confirmButtonColor : '#3085d6',
        confirmButtonText :'حسنا',
        cancelButtonColor : '#d33',
       
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/admin/accounts/DeleteRole?id=${id}`;
            Swal.fire(
                'Deleted!',
                'Your file has been delete',
                'success'
                )
        }
    })
        
}


function Edit(id, name) {
    document.getElementById("RoleId").value = id;
    document.getElementById("RoleName").value = name;
    document.getElementById("btnAction").value = "تعديل";
    document.getElementById("exampleModalLabel").innerHTML = "تعديل اسم المجموعة";


}

Rest = () => {
    document.getElementById("RoleId").value = null;
    document.getElementById("RoleName").value = "";
    document.getElementById("btnAction").value = "حفظ";
    document.getElementById("exampleModalLabel").innerHTML = "اضافة مجموعة جديدة ";
}


