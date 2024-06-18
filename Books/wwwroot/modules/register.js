
alert(1234567);

$(document).ready(function () {
    $('#registertable').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});


function DeleteUser(id) {
    Swal.fire({
        title: deleteTitle,
        text: deleteContentCheck,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        confirmButtonText: ok,
        cancelButtonColor: '#d33',

    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/admin/accounts/DeleteUser?id=${id}`;
            Swal.fire(
                deleteTitle,
                deleteContent,
                'success'
            )
        }
    })

}



function EditUser(id, name, Email, ImageUser, Role, ActiveUser) {

    alert(ImageUser)
    document.getElementById("UserId").value = id;
    document.getElementById("UserName").value = name;
    document.getElementById("UserEmail").value = Email;
    document.getElementById("UserRole").value = Role;

    document.getElementById("btnAction").value = btnEdit;
    document.getElementById("exampleModalLabel").innerHTML = editContent;
    var active = document.getElementById("Useractive");
    if (ActiveUser == "True")
        active.checked = true;
    else
        active.checked = false;

    document.getElementById('UserConfPasswordg').hidden = true;
    document.getElementById('UserPasswordg').hidden = true;
    document.getElementById("UserPassword").value = '#####';
    document.getElementById("UserConfPassword").value = "#####";
    document.getElementById("Image").hidden = false;
    document.getElementById("HideImageUser").value = ImageUser;
    document.getElementById("Image").src = imagePath + ImageUser;

}


function Rest() {

    document.getElementById("UserId").value = "";
    document.getElementById("UserName").value = "";
    document.getElementById("UserEmail").value = "";
    document.getElementById("UserRole").value = role;
    document.getElementById("exampleModalLabel").innerHTML = modelTitle;
    document.getElementById("Useractive").checked = false;
    document.getElementById('UserConfPasswordg').hidden = false;
    document.getElementById('UserPasswordg').hidden = false;
    document.getElementById("UserPassword").value = ''
    document.getElementById("UserConfPassword").value = ''
    document.getElementById("Image").hidden = true;
    document.getElementById("HideImageUser").value = "a.jpg";
}

PasswordRecovery = (id) => {
    document.getElementById("UserNewPasswordId").value = id;
}

