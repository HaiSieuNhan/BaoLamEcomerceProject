function updateProfile(form) {
    $.ajax({
        type: 'post',
        dataType: "json",
        async: false,
        url: form.action,
        data: new FormData(form),
        contentType: false,
        processData: false,
        success: function (res) {
            $(".customer-profile .update-profile-success").remove();
            $(".customer-profile .update-profile-error").remove();
            // render lại trang
            $(".colorlib-nav").next().remove();
            $(".colorlib-nav").after(res.html);
            $('[data-toggle="datepicker"]').datepicker({ format: 'dd/mm/yyyy' }).val();
            if (res.isValid == true) {
                $(".customer-profile").prepend('<div class="update-profile-success">' + res.msg + '</div >')
            } else {

                $(".customer-profile").prepend('<div class="update-profile-error">' + res.msg + '</div >');             
            }

        },
        error: function (err) {
            console.log(err);
        }
    })
    return false;
}