function Add(Id, Name, Action, Controller, AddModel, loading) {
    AddModel = $("#AddModel_");
    loading = $("#loading");
    debugger;
    loading.removeClass("loadingImg");
    AddModel.parent().addClass("Popuploading");
    var Bname = Name;
    if (name !== "") {
        Bname = name;
    }

    if (Id > 0) {
        $.ajax({
            url: "/" + Controller + "/" + Action + "",
            type: 'GET',
            data: { id: Id },
            success: function (result) {
                $("#AddModel_").html(result);
                Bname = Action + ' ' + Name;
                $('#name').html(Bname);
                loading.addClass("loadingImg");
                AddModel.parent().removeClass("Popuploading");
                $(".modal").css("display", "block");
            }
        });
    }
    else {
        $.ajax({
            url: "/" + Controller + "/" + Action + "",
            type: 'GET',
            success: function (result) {
                $("#AddModel_").html(result);
                Bname = Action + ' ' + Name;
                $('#name').html(Bname);
                $("#loading").addClass("loadingImg");
                AddModel.parent().removeClass("Popuploading");
                $(".modal").css("display", "block");
            }
        });
    }
}

function SubmitForm(frm, res) {
    $('#loading-img-save').show();
    document.getElementById('save').disabled = "true";
    $.ajax({
        url: frm.action,
        type: 'POST',
        data: new FormData(frm),
        processData: false,
        contentType: false,
        success: function (responseText, textStatus, XMLHttpRequest) {
            $('#AddModel_').html(responseText);
            var generalErrors = $(".validation-summary-errors li");
            
                $(".input-validation-error").tooltip({
                    trigger: 'focus',
                    placement: 'top'
                });
                $(".input-validation-error")[0].focus();
           
            var message = res;
            if (message.length > 0) {
                document.getElementById('name').focus();
            }
        }
    });
    return false;
}

