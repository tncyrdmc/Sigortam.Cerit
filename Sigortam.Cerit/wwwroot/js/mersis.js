function identityCheck() {
    let $form = $("#form");
    if ($form.length === 0) {
        return null;
    }

    var forms = document.querySelectorAll('.needs-validation-text');
    var validate = 0;
    for (var i = 0; i < forms.length; i++) {
        if (!(forms[i].value)) {
            validate++;
        }
    }
    //var forms2 = document.querySelectorAll('.needs-validation').filter(x => x.id != "insuranceStartDate" || x.id != "insuranceEndDate")
    debugger;
    if (validate === 0) {
        var userIdentityCheckDto = {
            Name: document.getElementById("name").value, LastName: document.getElementById("lastName").value, IdentificationNumber: document.getElementById("identificationNumber").value, BirthDate: document.getElementById("birthDate").value,
            InsuranceStartDate: document.getElementById("insuranceStartDate").value, InsuranceEndDate: document.getElementById("insuranceEndDate").value, PlateNumber: document.getElementById("plateNumber").value
        };

        $.ajax({
            type: "POST",
            url: "/Mernis/IdentityCheck/",
            data: userIdentityCheckDto,
            //contentType: "application/json; charset=utf-8",
            //dataType: "json",
            success: async function (result) {
                debugger;
                if (result.code == 101) {
                    alert(result.message);
                    location.href = "/Home/Index";
                }
                else {
                    alert(result.message)
                }
            }
        });
    }

    //$.post("Mernis/IdentityCheck", { userIdentityCheckDto: userIdentityCheckDto }, function (result) {
    //        debugger;
    //        if (result.code == 101) {
    //            location.href = "/Home/Index";
    //        }
    //        else {
    //            location.href = "/UserLogin/Index";
    //        }
    //        //if (result == "Succeeded") {
    //        //    $(".card-alert .close").click(function () {
    //        //        $(this).closest(".card-alert").fadeOut("slow");
    //        //    });
    //        //}
    //    });
}
